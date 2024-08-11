namespace Tranchy.News.Contract;

public interface INewsReader
{
    Task<IEnumerable<NewsData>> FetchNewsAsync(CancellationToken cancellationToken);
}
