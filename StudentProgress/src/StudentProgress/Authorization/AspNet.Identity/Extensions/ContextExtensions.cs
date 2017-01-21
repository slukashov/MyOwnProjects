using Microsoft.EntityFrameworkCore;
namespace StudentProgress.Authorization.AspNet.Identity.Extensions
{
    internal static class ContextExtensions
    {
        internal static void Update<T>(this DbContext context, T entity) where T : class
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
