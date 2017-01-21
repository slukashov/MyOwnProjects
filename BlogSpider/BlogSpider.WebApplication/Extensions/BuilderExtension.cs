using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogSpider.WebApplication.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Owin.Builder;
    using Owin;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseAppBuilder(
            this IApplicationBuilder applicationBuilder,
            Action<IAppBuilder> configure)
        {
            applicationBuilder.UseOwin(addToPipeline =>
            {
                addToPipeline(next =>
                {
                    var builder = new AppBuilder();
                    builder.Properties["builder.DefaultApp"] = next;

                    configure(builder);

                    return builder.Build<AppFunc>();
                });
            });
            return applicationBuilder;
        }

        public static void UseSignalR2(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAppBuilder(builder => builder.MapSignalR());
        }
    }
}
