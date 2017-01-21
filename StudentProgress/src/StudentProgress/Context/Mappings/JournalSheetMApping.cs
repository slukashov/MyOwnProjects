using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class JournalSheetMapping : IMappingInterface<JournalSheet>
    {
        public void MapEntity(EntityTypeBuilder<JournalSheet> builder)
        {
            builder.ForNpgsqlToTable("journalsheet");
            builder.HasKey(entity => entity.Id);
            builder.HasMany(entity => entity.ListOfFinalMarks);
            builder.HasMany(entity => entity.ListOfLessons);
            builder.HasMany(entity => entity.ListOfMarks);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.GroupId).HasColumnName("group_id");
            builder.Property(entity => entity.DisciplineId).HasColumnName("discipline");
            builder.Property(entity => entity.ProfessorId).HasColumnName("professor");
            builder.Property(entity => entity.Semester).HasColumnName("semester");
        }
    }
}
