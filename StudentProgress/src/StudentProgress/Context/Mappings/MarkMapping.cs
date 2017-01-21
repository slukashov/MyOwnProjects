using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class MarkMapping : IMappingInterface<Mark>
    {
        public void MapEntity(EntityTypeBuilder<Mark> builder)
        {
            builder.ForNpgsqlToTable("mark");
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.Rating).HasColumnName("rating");
            builder.Property(entity => entity.JournalSheetId).HasColumnName("journal_sheet");
            builder.Property(entity => entity.StudentId).HasColumnName("student");
            builder.Property(entity => entity.LessonId).HasColumnName("lesson_id");
        }
    }
}
