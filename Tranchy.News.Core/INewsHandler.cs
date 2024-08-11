using Tranchy.News.Contract;

namespace Blog_microkernel_in_action;

public interface INewsHandler
{
    Task<int> Handle(NewsData newsDataItem);
}