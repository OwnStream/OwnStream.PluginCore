namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// Metadata of an episode of a TV show
/// </summary>
public class MetadataEpisodeResult
{
	/// <summary>
	/// Episode title
	/// </summary>
	public required string Title { get; set; }
	/// <summary>
	/// Title translations (following a schema of "en_US" => "Title", "tr_TR" => "Başlık". Region is optional, keys can just be the language code ("en", "tr", "fr"...))
	/// </summary>
	public Dictionary<string, string>? TitleTranslations { get; set; }
	/// <summary>
	/// Episode summary
	/// </summary>
	public string? Summary { get; set; }
	/// <summary>
	/// Summary translations (following a schema of "en_US" => "Title", "tr_TR" => "Başlık". Region is optional, keys can just be the language code ("en", "tr", "fr"...))
	/// </summary>
	public Dictionary<string, string>? SummaryTranslations { get; set; }
	/// <summary>
	/// Episode release date. Must follow the ISO 8601 date and time format.
	/// </summary>
	public string? ReleaseDate { get; set; }
	/// <summary>
	/// 16:9 thumbnails for the episode.
	/// </summary>
	public MetadataImage[]? Thumbnails { get; set; }
}