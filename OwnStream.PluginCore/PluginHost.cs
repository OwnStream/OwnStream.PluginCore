using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using GlobExpressions;

namespace OwnStream.PluginCore;

public class PluginHost(PluginMeta meta)
{
	private readonly HttpClient httpClient = new();

	public async Task<object> Fetch(string url, string optionsStr)
	{
		bool allowed = meta.Permissions.Where(x => x.StartsWith("http"))
			.Any(pattern => 
				Glob.IsMatch(url, pattern));
		if (!allowed)
			throw new UnauthorizedAccessException($"Access to URL '{url}' is not granted.");

		JsonObject? options = JsonSerializer.Deserialize<JsonObject>(optionsStr);
		JsonObject headers = options?["headers"] as JsonObject ?? new JsonObject();

		HttpMethod method;
		try
		{
			method = HttpMethod.Parse(options?["method"]?.ToString() ?? "GET");
		}
		catch (Exception)
		{
			method = HttpMethod.Get;
		}

		HttpRequestMessage req = new(method, url);
		string? body = options?["body"]?.GetValue<string>();
		string? contentType = headers["Content-Type"]?.GetValue<string>();
		if (body != null)
			req.Content = new StringContent(body, Encoding.UTF8, contentType);

		foreach ((string key, JsonNode? value) in headers)
			if (value?.GetValueKind() == JsonValueKind.String && key != "Content-Type")
				req.Headers.Add(key, value.GetValue<string>());

		HttpResponseMessage resp = await httpClient.SendAsync(req);
		return new
		{
			status = resp.StatusCode,
			ok = resp.IsSuccessStatusCode,
			data = await resp.Content.ReadAsStringAsync(),
			headers = resp.Headers.ToDictionary(h => h.Key, h => h.Value.ToArray())
		};
	}

	public void Log(string message)
	{
		Console.WriteLine($"[JS] {message}");
	}

	public const string Polyfill =
		"""
		globalThis.fetch = async (resource, options) => {
		        const url = typeof resource === "object" ? resource.url : resource;
		        const resp = await host.Fetch(url, JSON.stringify(options));
		        return {
		                status: resp.status,
		                ok: resp.ok,
		                headers: resp.headers,
		                type: "basic",
		                json: () => new Promise((res) => res(JSON.parse(resp.data))),
		                text: () => new Promise((res) => res(resp.data)),
		        };
		}
		
		globalThis.log = (message) => host.Log(message);
		""";
}