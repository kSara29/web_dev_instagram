using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Persistence.Configurations;

public class LikeConfiguration: IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id);
        builder.Property(l => l.UserId);
        builder.Property(l => l.PostId);
        
        builder
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId);
    }
}