using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class DisciplineMapping : IMappingInterface<Discipline>
    {
        public void MapEntity(EntityTypeBuilder<Discipline> builder)
        {
            builder.ForNpgsqlToTable("discipline");
            builder.HasKey(entity => entity.Id);
            builder.HasMany(entity => entity.ListOfJournalSheets);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.Name).HasColumnName("name");
        }
    }
}
