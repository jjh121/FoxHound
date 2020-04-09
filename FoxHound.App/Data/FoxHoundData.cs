using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;

namespace FoxHound.App.Data
{
    public class FoxHoundData : DbContext, IFoxHoundData
    {
        public FoxHoundData(DbContextOptions<FoxHoundData> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoxHoundData).Assembly);
        }
    }
}