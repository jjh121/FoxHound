using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoxHound.App.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.CommentId);
            builder.Property(x => x.PostId).IsRequired();
            builder.Property(x => x.Author).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Content);
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.ToTable("Comments");
        }
    }
}