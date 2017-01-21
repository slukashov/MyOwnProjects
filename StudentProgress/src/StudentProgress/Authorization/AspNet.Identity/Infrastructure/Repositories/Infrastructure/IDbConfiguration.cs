namespace StudentProgress.Authorization.AspNet.Identity.Infrastructure.Repositories.Infrastructure
{
    public interface IDbConfiguration
    {
        bool ContextOwnsConnection { get; }
        bool EnableLazyLoading { get; }
        bool ProxyCreationEnabled { get; }
    }
}
