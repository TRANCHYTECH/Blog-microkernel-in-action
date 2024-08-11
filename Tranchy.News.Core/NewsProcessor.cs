using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tranchy.News.Contract;

namespace Blog_microkernel_in_action;

public class NewsProcessor: BackgroundService
{
    private const int MAX_HANDLERS_NUMBER = 5;
    private readonly IEnumerable<INewsReaderPlugin> _plugins;
    private readonly INewsHandler _newsHandler;
    private readonly ILogger<NewsProcessor> _logger;
    
    public NewsProcessor(IEnumerable<INewsReaderPlugin> plugins, INewsHandler newsHandler, ILogger<NewsProcessor> logger)
    {
        _plugins = plugins;
        _newsHandler = newsHandler;
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessData(stoppingToken);
        }

        return Task.CompletedTask;
    }

    private void ProcessData(CancellationToken stoppingToken)
    {
        var queue = new BlockingCollection<NewsData>();
        var tasks = Enumerable.Range(0, MAX_HANDLERS_NUMBER)
            .Select((i) => Task.Run(() =>
            {
                _logger.LogInformation($"Start handling from task #{i}");
                HandleNewsInQueue(queue);
                _logger.LogInformation($"Finish handling from task #{i}");
            }, stoppingToken))
            .ToArray();

        Parallel.ForEach(_plugins, (plugin) =>
        {
            var reader = plugin.GetReader();
            var news = reader.FetchNewsAsync(stoppingToken)
                .GetAwaiter()
                .GetResult();

            foreach (var newsItem in news)
            {
                queue.Add(newsItem, stoppingToken);
            }
        });
        
        queue.CompleteAdding();

        Task.WaitAll(tasks);
    }

    private void HandleNewsInQueue(BlockingCollection<NewsData> newsQueue)
    {
        foreach (var newsData in newsQueue.GetConsumingEnumerable())
        {
            _newsHandler.Handle(newsData);
        }
    }
}