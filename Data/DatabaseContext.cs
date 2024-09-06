using DotnetIdentityDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetIdentityDemo.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}