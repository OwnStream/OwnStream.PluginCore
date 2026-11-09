// ReSharper disable CollectionNeverUpdated.Global

namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// Shared fields between TV and Movie metadata. Do not access this class directly, use
/// <see cref="MetadataTvResult"/> and <see cref="MetadataMovieResult"/> instead.
/// </summary>
public class MetadataGenericResult
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
	/// Content tagline
	/// </summary>
	public string? Tagline { get; set; }
	/// <summary>
	/// Tagline translations (following a schema of "en_US" => "Tagline", "tr_TR" => "Yan Başlık". Region is optional, keys can just be the language code ("en", "tr", "fr"...))
	/// </summary>
	public Dictionary<string, string>? TaglineTranslations { get; set; }
	/// <summary>
	/// Content description
	/// </summary>
	public string? Description { get; set; }
	/// <summary>
	/// Description translations (following a schema of "en_US" => "Description", "tr_TR" => "Açıklama". Region is optional, keys can just be the language code ("en", "tr", "fr"...))
	/// </summary>
	public Dictionary<string, string>? DescriptionTranslations { get; set; }
	/// <summary>
	/// Content images
	/// </summary>
	public Dictionary<string, MetadataImage[]>? Images { get; set; }
	/// <summary>
	/// Content release date. Must follow the ISO 8601 date and time format.
	/// </summary>
	public string? ReleaseDate { get; set; }
	/// <summary>
	/// List of age ratings, mapped by region to string (example: "US" => "TV-PG", "TR" => "7+", "DE" => "16")
	/// </summary>
	public Dictionary<string, string>? AgeRatings { get; set; }
}