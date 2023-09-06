using BlogGrpc.Models;
namespace BlogGrpc.Data;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
     public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Blog> Blogs => Set<Blog>();

}
