using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings.Interface;

namespace StudentProgress.Context.Mappings
{
    public class IdentityRoleClaimMapping : IMappingInterface<IdentityRoleClaim>
    {
        public void MapEntity(EntityTypeBuilder<IdentityRoleClaim> builder)
        {
            builder.ForNpgsqlToTable("role_claims", "identity");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("role_claim_id");
            builder.Property(entity => entity.RoleId).HasColumnName("role_id");
            builder.Property(entity => entity.ClaimType).HasColumnName("claim_type");
            builder.Property(entity => entity.ClaimValue).HasColumnName("claim_value");
        }
    }
}
