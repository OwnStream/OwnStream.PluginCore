// ReSharper disable ClassNeverInstantiated.Global
namespace OwnStream.PluginCore.ResultTypes;

public class MetadataSeasonListResult
{
	public required string Id { get; set; }
	public required int Index { get; set; }
	public required string Name { get; set; }
	public MetadataImage[]? Posters { get; set; }
}