// ReSharper disable ClassNeverInstantiated.Global
namespace OwnStream.PluginCore.ResultTypes;

public class MetadataSearchResult
{
	public required string Id { get; set; }
	public required string[] Titles { get; set; }
	public string? Poster { get; set; }
	public int? Year { get; set; }
}