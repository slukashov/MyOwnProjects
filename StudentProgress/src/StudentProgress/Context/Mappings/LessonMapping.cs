using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class LessonMapping : IMappingInterface<Lesson>
    {
        public void MapEntity(EntityTypeBuilder<Lesson> builder)
        {
            builder.ForNpgsqlToTable("lesson");
            builder.HasKey(entity => entity.Id);
            builder.HasMany(entity => entity.ListOfStudents);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.Date).HasColumnName("date");
            builder.Property(entity => entity.JournalSheetId).HasColumnName("journal_sheet");
        }
    }
}
