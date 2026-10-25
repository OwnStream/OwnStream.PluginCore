// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace OwnStream.PluginCore.ResultTypes;

public class MetadataSeasonResult
{
	public required string Title { get; set; }
	public Dictionary<string, string>? TitleTranslations { get; set; }
	public string? Description { get; set; }
	public Dictionary<string, string>? DescriptionTranslations { get; set; }
	public MetadataImage[]? Posters { get; set; }
}