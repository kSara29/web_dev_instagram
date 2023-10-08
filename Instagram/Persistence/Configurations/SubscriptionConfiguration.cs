using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Persistence.Configurations;

public class SubscriptionConfiguration: IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id);
        builder.Property(s => s.TargetUserId);
        builder.Property(s => s.SubscriberId);
        
        builder
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(s => s.TargetUser)
            .WithMany(u => u.Followers)
            .HasForeignKey(s => s.TargetUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}