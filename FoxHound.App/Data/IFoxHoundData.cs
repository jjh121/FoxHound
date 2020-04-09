using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Data
{
    public interface IFoxHoundData
    {
        DbSet<Blog> Blogs { get; set; }

        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}