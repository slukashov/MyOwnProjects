using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings.Interface;

namespace StudentProgress.Context.Mappings
{
    public class IdentityUserMapping : IMappingInterface<IdentityUser>
    {
        public void MapEntity(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.ForNpgsqlToTable("users", "identity");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("user_id");
            builder.Property(entity => entity.ConcurrencyStamp).HasColumnName("concurrency_stamp").IsConcurrencyToken();
            builder.Property(entity => entity.UserName).HasColumnName("username").IsRequired();
            builder.Property(entity => entity.NormalizedUserName).HasColumnName("normalized_username");
            builder.Property(entity => entity.Email).HasColumnName("email").IsRequired();
            builder.Property(entity => entity.NormalizedEmail).HasColumnName("normalized_email");
            builder.Property(entity => entity.EmailConfirmed).HasColumnName("email_confirmed");
            builder.Property(entity => entity.PasswordHash).HasColumnName("password_hash").IsRequired();
            builder.Property(entity => entity.SecurityStamp).HasColumnName("security_stamp");
            builder.Property(entity => entity.PhoneNumber).HasColumnName("phone_number");
            builder.Property(entity => entity.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            builder.Property(entity => entity.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            builder.Property(entity => entity.LockoutEnd).HasColumnName("lockout_end");
            builder.Property(entity => entity.LockoutEnabled).HasColumnName("lockout_enabled");
            builder.Property(entity => entity.AccessFailedCount).HasColumnName("access_failed_count");
            builder.HasMany(entity => entity.Claims).WithOne().HasForeignKey(entity => entity.UserId);
            builder.HasMany(entity => entity.Logins).WithOne().HasForeignKey(entity => entity.UserId);
            builder.HasMany(entity => entity.Roles).WithOne().HasForeignKey(entity => entity.UserId);
        }
    }
}
