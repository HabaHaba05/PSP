using LibraryUsage.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryUsage.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
