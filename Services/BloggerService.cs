using Grpc.Core;
using BlogGrpc;
using BlogGrpc.Data;
using BlogGrpc.Models;
using Microsoft.EntityFrameworkCore;
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

     public override async Task<getAllResponse> ListBlog(getAllRequest request, ServerCallContext context)
    {
        var response = new getAllResponse();

        var blogs = await _db.Blogs.ToListAsync();

        foreach(var blog in blogs)
        {
            // response.ToDo.Add(new ReadToDoResponse{
            //     Id = toDo.Id,
            //     Title = toDo.Title,
            //     Description = toDo.Description,
            //     ToDoStatus = toDo.ToDoStatus
            // });
        }

        return await Task.FromResult(response);
    }

}