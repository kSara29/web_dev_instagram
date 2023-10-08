using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Persistence.Configurations;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id);
        builder.Property(p => p.UserId);
        builder.Property(p => p.Image);
        builder.Property(p => p.Description);
        
    }
}