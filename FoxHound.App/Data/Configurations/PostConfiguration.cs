using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoxHound.App.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.PostId);
            builder.Property(x => x.BlogId).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Content);
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId);

            builder.ToTable("Posts");
        }
    }
}