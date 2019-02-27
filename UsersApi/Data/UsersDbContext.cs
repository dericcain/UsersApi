using Microsoft.EntityFrameworkCore;
using UsersApi.Models;

namespace UsersApi.Data
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }
    }
}