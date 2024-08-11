using Microsoft.Extensions.DependencyInjection;

namespace Tranchy.News.Contract;

public interface INewsReaderPlugin
{
    INewsReader GetReader();
}