using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class LessonStudentMapping : IMappingInterface<LessonStudent>
    {
        public void MapEntity(EntityTypeBuilder<LessonStudent> builder)
        {
            builder.ForNpgsqlToTable("lesson_student");
            builder.HasKey(entity => new { entity.LessonId, entity.StudentId });
            builder
                .HasOne(pc => pc.Lesson)
                .WithMany(group => group.ListOfStudents)
                .HasForeignKey(key => key.LessonId);
            builder
                .HasOne(pc => pc.Student)
                .WithMany(student => student.ListOfLessonss)
                .HasForeignKey(key => key.StudentId);
            builder.Property(entity => entity.LessonId).HasColumnName("lesson");
            builder.Property(entity => entity.StudentId).HasColumnName("student");
            builder.Property(entity => entity.Attending).HasColumnName("attending");
        }
    }
}
