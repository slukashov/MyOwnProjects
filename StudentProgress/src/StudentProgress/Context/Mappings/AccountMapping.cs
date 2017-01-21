using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class AccountMapping : IMappingInterface<Account>
    {
        public void MapEntity(EntityTypeBuilder<Account> builder)
        {
            builder.ForNpgsqlToTable("account");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.Name).HasColumnName("name");
            builder.Property(entity => entity.SerName).HasColumnName("sername");
            builder.Property(entity => entity.Email).HasColumnName("email");
            builder.Property(entity => entity.Password).HasColumnName("password");
        }
    }
}
