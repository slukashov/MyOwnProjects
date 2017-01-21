using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings.Interface;

namespace StudentProgress.Context.Mappings
{
    public class IdentityUserLoginMapping : IMappingInterface<IdentityUserLogin>
    {
        public void MapEntity(EntityTypeBuilder<IdentityUserLogin> builder)
        {
            builder.ForNpgsqlToTable("user_logins", "identity");
            builder.HasKey(entity => new { entity.LoginProvider, entity.ProviderKey });
            builder.Property(entity => entity.LoginProvider).HasColumnName("login_provider").IsRequired();
            builder.Property(entity => entity.ProviderKey).HasColumnName("provider_key")
                .IsRequired();
            builder.Property(entity => entity.ProviderDisplayName).HasColumnName("provider_display_name");
            builder.Property(entity => entity.UserId).HasColumnName("user_id")
                .IsRequired();
        }
    }
}
