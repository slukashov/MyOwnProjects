using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;
namespace StudentProgress.Context.Mappings
{
    public class FacultyMapping : IMappingInterface<Faculty>
    {
        public void MapEntity(EntityTypeBuilder<Faculty> builder)
        {
            builder.ForNpgsqlToTable("faculty");
            builder.HasKey(entity => entity.Id);
            builder.HasMany(entity => entity.ListOfProfessors);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.Name).HasColumnName("name");
        }
    }
}
