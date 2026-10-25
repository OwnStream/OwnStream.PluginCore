// ReSharper disable CollectionNeverUpdated.Global
namespace OwnStream.PluginCore.ResultTypes;

public class MetadataGenericResult
{
	public required string Title { get; set; }
	public Dictionary<string, string>? TitleTranslations { get; set; }
	public string? Tagline { get; set; }
	public Dictionary<string, string>? TaglineTranslations { get; set; }
	public string? Description { get; set; }
	public Dictionary<string, string>? DescriptionTranslations { get; set; }
	public Dictionary<string, MetadataImage[]>? Images { get; set; }
	public string? ReleaseDate { get; set; }
	public Dictionary<string, string>? AgeRatings { get; set; }
}