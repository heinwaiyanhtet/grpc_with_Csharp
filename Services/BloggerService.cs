using Grpc.Core;
using BlogGrpc;
namespace BlogGrpc.Services;

public class BloggerService : Blogger.BloggerBase
{
    private readonly ILogger<BloggerService> _logger;

    public BloggerService (ILogger<BloggerService> logger)
    {
        _logger = logger;   
    }


}