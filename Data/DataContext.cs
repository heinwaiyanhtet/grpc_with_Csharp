// using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using BlogGrpc.Models;

namespace ToDoGrpc.Data;

public class AppDbContext : DbContext
{
    // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    // {
        
    // }

    public DbSet<Blog> blogs { get; set; } = default!;
}