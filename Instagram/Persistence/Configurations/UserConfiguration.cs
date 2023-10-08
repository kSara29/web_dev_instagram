using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Instagram.Models;

namespace Instagram.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id);
        builder.Property(u => u.Avatar);
        builder.Property(u => u.Name);
        builder.Property(u => u.Description);
        builder.Property(u => u.Gender);
        builder.Property(u => u.UserName);
        builder.Property(u => u.NormalizedUserName);
        builder.Property(u => u.Email);
        builder.Property(u => u.NormalizedEmail);
        builder.Property(u => u.EmailConfirmed);
        builder.Property(u => u.PasswordHash);
        builder.Property(u => u.SecurityStamp);
        builder.Property(u => u.ConcurrencyStamp);
        builder.Property(u => u.PhoneNumber);
        builder.Property(u => u.PhoneNumberConfirmed);
        builder.Property(u => u.TwoFactorEnabled);
        builder.Property(u => u.LockoutEnd);
        builder.Property(u => u.LockoutEnabled);
        builder.Property(u => u.AccessFailedCount);
    }
}