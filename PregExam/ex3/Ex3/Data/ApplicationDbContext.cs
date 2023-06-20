using Ex3.Models;
using Microsoft.EntityFrameworkCore;

namespace Ex3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Post> Post { get; set; }
    }
}
