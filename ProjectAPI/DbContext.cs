using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI
{
    public class Mydatabase : DbContext
    {
        public Mydatabase(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
