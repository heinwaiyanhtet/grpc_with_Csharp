using Grpc.Core;
using BlogGrpc;
using BlogGrpc.Data;
using BlogGrpc.Models;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;
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

            var blog = new Blog
            {
                Title = request.Title,
                Author = request.Author,
                ImageUrl  = request.ImageUrl,
                Description = request.Description
            };

            await _db.AddAsync(blog);
            await  _db.SaveChangesAsync();

            return await Task.FromResult(new CreateBlogResponse
            {
                Id = 1,
            });
    }


     public override async Task<getAllResponse> ListBlog (getAllRequest request, ServerCallContext context)
    {
        var response = new getAllResponse();
        
        var blogs = await _db.Blogs.ToListAsync();

        foreach(var blog in blogs)
        {
            var blogInfo = new BlogInfo
            {
                Title = blog.Title,
                Author = blog.Author,
                ImageUrl  = blog.ImageUrl,
                Description = blog.Description
            };
          response.Blogs.Add(blogInfo);

        }
        return response;        
    }

    public override async Task<GetBlogByIdResponse> GetBlogById (GetBlogByIdRequest request,ServerCallContext context)
    {
        var blog = await _db.Blogs.FindAsync(request.Id);
        
        if(blog == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,$"Blog with ID {request.Id} not found"));
        }

        var blogInfo = new BlogInfo
        {
            Title = blog.Title,
            Author = blog.Author,
            ImageUrl  = blog.ImageUrl,
            Description = blog.Description
        };

        var response = new GetBlogByIdResponse
        {
            Blog = blogInfo,
        };
        
        return response;

    }

    public override async Task<DeleteBlogResponse> DeleteBlog (DeleteBlogRequest request,ServerCallContext context)
    {
        var blog = await _db.Blogs.FindAsync(request.Id);

        if(blog == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Blog with ID {request.Id} not found"));
        }

         _db.Blogs.Remove(blog);
         await _db.SaveChangesAsync();

         return new DeleteBlogResponse
         {
            Success = true,
         };
    }

    

}