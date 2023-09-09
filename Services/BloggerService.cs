using Grpc.Core;
using BlogGrpc;
using BlogGrpc.Data;
namespace BlogGrpc.Services;

public class BloggerService : Blogger.BloggerBase
{
    private readonly ILogger<BloggerService> _logger;
    private readonly DataContext _db;
    public BloggerService (ILogger<BloggerService> logger,DataContext dataContext)
    {
        _logger = logger;   
        _db = dataContext;
    }

    public override async Task<CreateBlogResponse> CreateBlog(CreateBlogRequest request,ServerCallContext context)
    {

        if(request.Title == string.Empty || request.Description == string.Empty)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "You must suppply a valid object"));

        


         return await Task.FromResult(new CreateBlogResponse
        {
            Id = 1,
        });

    }

}