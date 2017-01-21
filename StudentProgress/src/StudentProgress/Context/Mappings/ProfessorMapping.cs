using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class ProfessorMapping : IMappingInterface<Professor>
    {
        public void MapEntity(EntityTypeBuilder<Professor> builder)
        {
            builder.ForNpgsqlToTable("professor");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.AccountId).HasColumnName("account_id");
            builder.Property(entity => entity.FacultyId).HasColumnName("faculty_id");
        }
    }
}
