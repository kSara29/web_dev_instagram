using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Persistence.Configurations;

public class PostCommentConfiguration: IEntityTypeConfiguration<PostComment>
{
    public void Configure(EntityTypeBuilder<PostComment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id);
        builder.Property(c => c.UserId);
        builder.Property(c => c.PostId);
        builder.Property(c => c.Comment);
        
        builder
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId);
    }
}