using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace OwnStream.PluginCore;

public class PluginMeta
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }
	[JsonPropertyName("version")]
	public required PluginVersion Version { get; set; }
	[JsonPropertyName("image")]
	public string? Image { get; set; }
	[JsonPropertyName("permissions")]
	public string[] Permissions { get; set; } = [];
	[JsonPropertyName("features")]
	public string[] Features { get; set; } = [];
	[JsonPropertyName("homepage")]
	public string? Homepage { get; set; }
	[JsonPropertyName("source")]
	public string? Source { get; set; }
	[JsonPropertyName("issues")]
	public string? Issues { get; set; }
	[JsonPropertyName("autoUpdater")]
	public AutoUpdaterConfig? AutoUpdater { get; set; }
}

public class PluginVersion
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }
	[JsonPropertyName("code")]
	public required int Code { get; set; }
}

public class AutoUpdaterConfig
{
	[JsonPropertyName("source")]
	public required string Source { get; set; }
	[JsonPropertyName("params")]
	public Dictionary<string, string> Params { get; set; } = [];
}