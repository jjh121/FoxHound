using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoxHound.App.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.BlogId);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Owner).IsRequired().HasMaxLength(20);
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasMany(x => x.Posts).WithOne().HasForeignKey(x => x.BlogId);

            builder.ToTable("Blogs");
        }
    }
}