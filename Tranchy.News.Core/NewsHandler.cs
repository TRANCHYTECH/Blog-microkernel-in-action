using Microsoft.Extensions.Logging;
using Tranchy.News.Contract;

namespace Blog_microkernel_in_action;

public class NewsHandler(ILogger<NewsHandler> logger) : INewsHandler
{
    public Task<int> Handle(NewsData newsDataItem)
    {
        logger.LogInformation($"Item {newsDataItem.Title} is handled");
        
        return Task.FromResult(1);
    }
}