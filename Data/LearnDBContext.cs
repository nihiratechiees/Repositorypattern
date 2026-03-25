using Microsoft.EntityFrameworkCore;
using Repopattern.Model;

namespace Repopattern.Data
{
    public class LearnDBContext:DbContext
    {
        public LearnDBContext(DbContextOptions<LearnDBContext> options) : base(options) { }

        public DbSet<Associate> associates { get; set; } = default!;
        public DbSet<User> user { get; set; } = default!;
    }
}
