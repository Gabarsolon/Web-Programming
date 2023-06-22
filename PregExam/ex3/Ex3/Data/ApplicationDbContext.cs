using Ex3.Models;
using Microsoft.EntityFrameworkCore;

namespace Ex3.Data
{
    public class ApplicationDbContext : DbContext
    {
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Users> users { get; set; }
        public DbSet<Content> content { get; set; }
    }
}