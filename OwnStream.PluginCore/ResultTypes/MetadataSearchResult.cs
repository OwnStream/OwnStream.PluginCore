// ReSharper disable ClassNeverInstantiated.Global
namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// Search result item
/// </summary>
public class MetadataSearchResult
{
	/// <summary>
	/// ID of the search result.
	/// </summary>
	public required string Id { get; set; }
	/// <summary>
	/// List of titles for this item. First index must be the main title,
	/// with localized versions/alternative titles/acronyms following it.
	/// </summary>
	public required string[] Titles { get; set; }
	/// <summary>
	/// Small poster preview URL (2:3 aspect ratio).
	/// </summary>
	public string? Poster { get; set; }
	/// <summary>
	/// Release year.
	/// </summary>
	public int? Year { get; set; }
}