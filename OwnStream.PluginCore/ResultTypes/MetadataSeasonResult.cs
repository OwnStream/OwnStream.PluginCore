// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// Full metadata for a season
/// </summary>
public class MetadataSeasonResult
{
	/// <summary>
	/// Content title
	/// </summary>
	public required string Title { get; set; }
	/// <summary>
	/// Title translations (following a schema of "en_US" => "Title", "tr_TR" => "Başlık". Region is optional, keys can just be the language code ("en", "tr", "fr"...))
	/// </summary>
	public Dictionary<string, string>? TitleTranslations { get; set; }
	/// <summary>
	/// Content description
	/// </summary>
	public string? Description { get; set; }
	/// <summary>
	/// Description translations (following a schema of "en_US" => "Description", "tr_TR" => "Açıklama". Region is optional, keys can just be the language code ("en", "tr", "fr"...))
	/// </summary>
	public Dictionary<string, string>? DescriptionTranslations { get; set; }
	/// <summary>
	/// 2:3 list of posters for this season
	/// </summary>
	public MetadataImage[]? Posters { get; set; }
}