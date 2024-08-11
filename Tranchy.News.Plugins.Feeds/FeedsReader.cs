using CodeHollow.FeedReader;
using Tranchy.News.Contract;

namespace Tranchy.News.Plugins.Feeds;

public class FeedsReader: INewsReader
{
    public async Task<IEnumerable<NewsData>> FetchNewsAsync(CancellationToken cancellationToken)
    {
        var feed = await FeedReader.ReadAsync("https://thanhnien.vn/rss/home.rss");

        return feed.Items.Select(i => new NewsData()
        {
            Id = Guid.NewGuid(),
            SourceId = nameof(FeedsReader),
            Title = i.Title,
            Description = i.Description,
            Link = i.Link
        });
    }
}