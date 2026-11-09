namespace OwnStream.PluginCore.ResultTypes;

/// <summary>
/// An image with a locale property and a score for ordering
/// </summary>
public class MetadataImage
{
	/// <summary>
	/// Direct URL to the image.
	/// </summary>
	public required string Url { get; set; }
	/// <summary>
	/// Image locale, if it includes text (should follow the format of <code>en_US</code> or just <code>en</code>)
	/// </summary>
	public string? Locale { get; set; }
	/// <summary>
	/// Image score (user votes in the upstream source, etc. Float between 0-1.)
	/// </summary>
	public float? Score { get; set; }
}