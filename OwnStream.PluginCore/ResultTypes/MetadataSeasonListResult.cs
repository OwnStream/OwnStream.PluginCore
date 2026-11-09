// ReSharper disable ClassNeverInstantiated.Global
namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// Season list item. Actual season details are received in <see cref="MetadataSeasonResult"/>
/// </summary>
public class MetadataSeasonListResult
{
	/// <summary>
	/// Season index number
	/// </summary>
	public required int Index { get; set; }
	/// <summary>
	/// Title of the season
	/// </summary>
	public required string Name { get; set; }
	/// <summary>
	/// Small poster preview URL (2:3 aspect ratio).
	/// </summary>
	public string? Poster { get; set; }
}