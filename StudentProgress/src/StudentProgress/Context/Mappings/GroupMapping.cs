using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class GroupMapping : IMappingInterface<Group>
    {
        public void MapEntity(EntityTypeBuilder<Group> builder)
        {
            builder.ForNpgsqlToTable("group");
            builder.HasKey(entity => entity.Id);
            builder.HasMany(entity => entity.ListOfStudents);
            builder.HasMany(entity => entity.ListOfJournalSheets);
            builder.Property(entity => entity.CapitainId).HasColumnName("capitain");
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.FacultyId).HasColumnName("faculty");
            builder.Property(entity => entity.Name).HasColumnName("name");
        }
    }
}
