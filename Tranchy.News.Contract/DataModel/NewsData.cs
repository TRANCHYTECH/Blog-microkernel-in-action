namespace Tranchy.News.Contract;

public struct NewsData
{
    public required Guid Id { get; set; }
    public required string SourceId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Link { get; set; }
}
