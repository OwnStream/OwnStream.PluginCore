using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace OwnStream.PluginCore;

/// <summary>
/// Instance of a loaded plugin.
/// </summary>
public class PluginInstance : IDisposable
{
	public PluginMeta Meta { get; private set; }
	private V8Runtime Runtime { get; }
	private V8ScriptEngine Engine { get; }
	private PluginHost PluginHost { get; }
	private dynamic PluginExports { get; set; }
	public ZipArchive? Bundle { get; private set; }

	/// <summary>
	/// Load a plugin from a path to its JavaScript code.
	/// </summary>
	/// <param name="jsPath">Path to the JavaScript code.</param>
	/// <param name="meta">Plugin metadata.</param>
	public PluginInstance(string jsPath, PluginMeta meta)
	{
		Meta = meta;

		Runtime = new V8Runtime();
		Engine = Runtime.CreateScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion);
		PluginHost = new PluginHost(meta);
		Engine.AddHostObject("host", PluginHost);
		V8Script polyfillScript = Runtime.Compile(PluginHost.Polyfill);
		V8Script pluginScript = Runtime.Compile(File.ReadAllText(jsPath));

		Engine.Execute(polyfillScript);
		Engine.Execute(pluginScript);
		PluginExports = Engine.Evaluate("getPlugin()");
	}

	/// <summary>
	/// Load a plugin from a ZIP bundle.
	/// </summary>
	/// <param name="bundlePath">Path to the plugin ZIP archive</param>
	public PluginInstance(string bundlePath)
	{
		Bundle = ZipFile.OpenRead(bundlePath);
		Meta = JsonSerializer.Deserialize<PluginMeta>(Bundle.Comment)!;

		ZipArchiveEntry? codeEntry = Bundle.GetEntry(Meta.Index);
		if (codeEntry == null)
			throw new FileNotFoundException($"Index file '{Meta.Index}' not found in the file.");

		Runtime = new V8Runtime();
		Engine = Runtime.CreateScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion);
		PluginHost = new PluginHost(Meta);
		Engine.AddHostObject("host", PluginHost);
		V8Script polyfillScript = Runtime.Compile(PluginHost.Polyfill);

		using Stream codeStream = codeEntry.Open();
		using StreamReader reader = new(codeStream);
		V8Script pluginScript = Runtime.Compile(reader.ReadToEnd());

		Engine.Execute(polyfillScript);
		Engine.Execute(pluginScript);
		PluginExports = Engine.Evaluate("getPlugin()");
	}

	/// <summary>
	/// Run a plugin method by feature path
	/// </summary>
	/// <param name="featurePath">Feature path of the method</param>
	/// <param name="args">Arguments to pass</param>
	/// <typeparam name="T">The type to deserialize the JavaScript return value to.</typeparam>
	/// <exception cref="InvalidOperationException">An invalid feature path was requested.</exception>
	public async Task<T?> InvokeFeature<T>(string featurePath, params object[] args)
	{
		if (!Meta.Features.Contains(featurePath))
			throw new InvalidOperationException($"Plugin does not expose feature '{featurePath}'.");
		string[] segments = featurePath.Split('.');

		object current = PluginExports;
		for (int i = 0; i < segments.Length - 1; i++)
		{
			if (current is not ScriptObject scriptObject)
				throw new InvalidOperationException($"'{segments[i]}' is not an object.");

			current = scriptObject.GetProperty(segments[i]);
		}

		if (current is not ScriptObject target)
			throw new InvalidOperationException($"Feature '{featurePath}' not found.");

		object? result = target.InvokeMethod(segments[^1], args);

		switch (result)
		{
			case Task task:
				await task;
				result = task.GetType().GetProperty("Result")?.GetValue(task);
				break;
			case ValueTask valueTask:
				await valueTask;
				result = valueTask.GetType().GetProperty("Result")?.GetValue(valueTask);
				break;
		}

		if (result == null) return default;
		// Serialize and deserialize to strip V8 internals
		// future kuylar here: i dont remember why i did this
		// but honestly the comment is so scary that im leaving this as is
		string json = JsonSerializer.Serialize(result, options);
		return JsonSerializer.Deserialize<T>(json, options);
	}

	/// <summary>
	/// Run a plugin method by feature path
	/// </summary>
	/// <param name="featurePath">Feature path of the method</param>
	/// <param name="args">Arguments to pass</param>
	/// <exception cref="InvalidOperationException">An invalid feature path was requested.</exception>
	public async Task<object?> InvokeFeature(string featurePath, params object[] args) =>
		await InvokeFeature<object>(featurePath, args);

	private static JsonSerializerOptions options = new()
	{
		AllowDuplicateProperties = true,
		DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
		UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
		WriteIndented = false,
		IndentCharacter = '\t',
		IndentSize = 1
	};

	public void Dispose()
	{
		Engine.Dispose();
		Runtime.Dispose();
		Bundle?.Dispose();
	}
}