namespace OwnStream.PluginCore.ResultTypes;

public class MetadataEpisodeResult
{
	public required string Title { get; set; }
	public Dictionary<string, string>? TitleTranslations { get; set; }
	public string? Summary { get; set; }
	public Dictionary<string, string>? SummaryTranslations { get; set; }
	public string? ReleaseDate { get; set; }
	public MetadataImage[]? Thumbnails { get; set; }
}