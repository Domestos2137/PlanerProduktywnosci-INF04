using Microsoft.EntityFrameworkCore;
using ProductivityPlanner.Api.Models;

namespace ProductivityPlanner.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoTask> Tasks { get; set; }

    }
}