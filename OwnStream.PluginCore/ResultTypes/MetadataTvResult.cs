// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// TV-specific metadata result.
/// </summary>
public class MetadataTvResult : MetadataGenericResult
{
	/// <summary>
	/// Last episode release date. Must follow the ISO 8601 date and time format. Must be null if the content is still receiving new episodes.
	/// </summary>
	public string? FinishDate { get; set; }
}