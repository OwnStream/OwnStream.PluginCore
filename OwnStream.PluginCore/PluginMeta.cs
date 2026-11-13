using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace OwnStream.PluginCore;

/// <summary>
/// Plugin metadata object. Contains all the required information to display and run a plugin.
/// </summary>
public class PluginMeta
{
	/// <summary>
	/// Plugin ID. Must match [a-z0-9_].
	/// </summary>
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	/// <summary>
	/// Plugin name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Plugin version.
	/// </summary>
	[JsonPropertyName("version")]
	public required PluginVersion Version { get; set; }

	/// <summary>
	/// Relative path to the JavaScript file in the bundle.
	/// </summary>
	[JsonPropertyName("index")]
	public required string Index { get; set; }

	/// <summary>
	/// URL that leads to a plugin icon. Must be at least 128x128 in size.
	/// If null, the <c>icon.png</c> file from the plugin root is used instead.
	/// </summary>
	[JsonPropertyName("image")]
	public string? Image { get; set; }

	/// <summary>
	/// Plugin permissions. Each entry can be one of:
	/// <list type="bullet">
	/// <item><term>a URL glob (e.g. <c>https://api.example.com/api/**</c>) - lets the plugin access the web via the fetch() call.</term></item>
	/// <item><term>metadata.provide.{id} - provides metadata for a external provider ID (metadata.provide.tmdb, metadata.provide.tvdb, etc.)</term></item>
	/// </list>
	/// </summary>
	[JsonPropertyName("permissions")]
	public string[] Permissions { get; set; } = [];

	/// <summary>
	/// Plugin features. Supported values:
	/// <list type="bullet">
	/// <item><term>metadata.search</term></item>
	/// <item><term>metadata.get.movie</term></item>
	/// <item><term>metadata.get.tv</term></item>
	/// <item><term>metadata.get.season</term></item>
	/// <item><term>metadata.get.tv.seasons</term></item>
	/// <item><term>metadata.get.episode</term></item>
	/// </list>
	/// If a feature is not advertised in this list, it will not be available.
	/// </summary>
	[JsonPropertyName("features")]
	public string[] Features { get; set; } = [];


	/// <summary>
	/// Link to the plugin homepage. For plugins that call an external service,
	/// this should be the link to that service, not a link to the plugin source.
	/// </summary>
	[JsonPropertyName("homepage")]
	public string? Homepage { get; set; }

	/// <summary>
	/// Link to the plugin source code
	/// </summary>
	[JsonPropertyName("source")]
	public string? Source { get; set; }

	/// <summary>
	/// Link to the plugin issues page
	/// </summary>
	[JsonPropertyName("issues")]
	public string? Issues { get; set; }

	/// <summary>
	/// Auto updater configuration
	/// </summary>
	[JsonPropertyName("autoUpdater")]
	public AutoUpdaterConfig? AutoUpdater { get; set; }
}

/// <summary>
/// Plugin version container.
/// </summary>
public class PluginVersion
{
	/// <summary>
	/// Visible plugin name (e.g. v1.2.3).
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>
	/// Integer version used for version comparison.
	/// </summary>
	[JsonPropertyName("code")]
	public required int Code { get; set; }
}

/// <summary>
/// Configuration for automatic plugin updates.
/// </summary>
public class AutoUpdaterConfig
{
	/// <summary>
	/// Auto-updater source type.
	/// </summary>
	[JsonPropertyName("type")]
	public required string Type { get; set; }

	/// <summary>
	/// Parameters for the auto-updater. See updater source for details.
	/// </summary>
	[JsonPropertyName("params")]
	public Dictionary<string, string> Params { get; set; } = [];
}