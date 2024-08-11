using Microsoft.Extensions.DependencyInjection;
using Tranchy.News.Contract;

namespace Tranchy.News.Plugins.Feeds;

public class FeedsReaderPlugin: INewsReaderPlugin
{
    public INewsReader GetReader()
    {
        return new FeedsReader();
    }
}