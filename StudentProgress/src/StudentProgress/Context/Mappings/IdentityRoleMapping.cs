using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings.Interface;

namespace StudentProgress.Context.Mappings
{
    public class IdentityRoleMapping : IMappingInterface<IdentityRole>
    {
        public void MapEntity(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ForNpgsqlToTable("roles", "identity");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("role_id");
            builder.Property(entity => entity.ConcurrencyStamp).HasColumnName("concurrency_stamp").IsConcurrencyToken();
            builder.Property(entity => entity.Name).HasColumnName("role_name");
            builder.Property(entity => entity.NormalizedName).HasColumnName("normalized_name");
            builder.HasMany(entity => entity.Users).WithOne().HasForeignKey(entity => entity.RoleId);
            builder.HasMany(entity => entity.Claims).WithOne().HasForeignKey(entity => entity.RoleId);
        }
    }
}
