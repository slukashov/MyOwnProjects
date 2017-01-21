using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgress.Context.Mappings.Interface;
using StudentProgress.Entities;

namespace StudentProgress.Context.Mappings
{
    public class StudentMapping : IMappingInterface<Student>
    {
        public void MapEntity(EntityTypeBuilder<Student> builder)
        {
            builder.ForNpgsqlToTable("student");
            builder.HasKey(entity => entity.Id);
            builder.HasMany(entity => entity.ListOfFinalMarks);
            builder.HasMany(entity => entity.ListOfMarks);
            builder.HasMany(entity => entity.ListOfLessonss);
            builder.Property(entity => entity.Id).HasColumnName("id");
            builder.Property(entity => entity.AccountId).HasColumnName("account_id");
            builder.Property(entity => entity.GroupId).HasColumnName("group_id");
        }
    }
}
