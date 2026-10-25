using System.Text.Json;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace OwnStream.PluginCore;

public class PluginInstance : IDisposable
{
	private PluginMeta Meta { get; }
	private V8Runtime Runtime { get; }
	private V8ScriptEngine Engine { get; }
	private PluginHost PluginHost { get; }
	private dynamic PluginExports { get; set; }

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

	public async Task<T?> InvokeFeature<T>(string featurePath, params object[] args)
	{
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
		string json = JsonSerializer.Serialize(result);
		return JsonSerializer.Deserialize<T>(json);
	}

	public async Task<object?> InvokeFeature(string featurePath, params object[] args) =>
		await InvokeFeature<object>(featurePath, args);

	public void Dispose()
	{
		Engine.Dispose();
		Runtime.Dispose();
	}
}