using System;
using StudentProgress.Authorization.AspNet.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StudentProgress.Context;

namespace StudentProgress.Authorization.AspNet.Identity.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static void AddEntityFrameworkStores(this IdentityBuilder builder)
        {
            builder.Services.AddUserStore(builder.UserType, builder.RoleType, typeof(StudentProgressContext));
            builder.Services.AddRoleStore(builder.UserType, builder.RoleType, typeof(StudentProgressContext));

            builder.AddDefaultTokenProviders();
        }

        private static void AddUserStore(this IServiceCollection serviceCollection, Type userType, Type roleType, Type contextType)
        {
            var userStoreType = typeof(UserStore<,,>).MakeGenericType(userType, roleType, contextType);
            serviceCollection.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
        }

        private static void AddRoleStore(this IServiceCollection serviceCollection, Type userType, Type roleType, Type contextType)
        {
            var roleStoreType = typeof(RoleStore<,>).MakeGenericType(roleType, contextType);
            serviceCollection.TryAddScoped(typeof(IRoleStore<>).MakeGenericType(roleType), roleStoreType);
        }
    }
}
