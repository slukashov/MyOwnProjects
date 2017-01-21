using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentProgress.Context.Mappings.Interface
{
    public interface IMappingInterface<T> where T : class
    {
        void MapEntity(EntityTypeBuilder<T> builder);
    }
}
