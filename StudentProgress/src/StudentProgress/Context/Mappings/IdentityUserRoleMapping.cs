using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings.Interface;

namespace StudentProgress.Context.Mappings
{
    public class IdentityUserRoleMapping : IMappingInterface<IdentityUserRole>
    {
        public void MapEntity(EntityTypeBuilder<IdentityUserRole> builder)
        {
            builder.ForNpgsqlToTable("users_roles", "identity");
            builder.HasKey(entity => new { entity.UserId, entity.RoleId });
            builder.Property(entity => entity.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(entity => entity.RoleId).HasColumnName("role_id").IsRequired();
        }
    }
}
