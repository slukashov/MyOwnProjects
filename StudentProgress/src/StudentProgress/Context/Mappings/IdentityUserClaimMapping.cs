using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Authorization.Entities.Identity;
using StudentProgress.Context.Mappings.Interface;

namespace StudentProgress.Context.Mappings
{
    public class IdentityUserClaimMapping : IMappingInterface<IdentityUserClaim>
    {
        public void MapEntity(EntityTypeBuilder<IdentityUserClaim> builder)
        {
            builder.ForNpgsqlToTable("user_claims", "identity");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("user_claim_id");
            builder.Property(entity => entity.ClaimType).HasColumnName("claim_type").IsRequired();
            builder.Property(entity => entity.ClaimValue).HasColumnName("claim_value");
            builder.Property(entity => entity.UserId).HasColumnName("user_id");
        }
    }
}
