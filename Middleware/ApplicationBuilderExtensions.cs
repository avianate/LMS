using System.IO;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microsoft.AspNet.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBowerComponents(this IApplicationBuilder app, IApplicationEnvironment env)
        {
            var path = Path.Combine(env.ApplicationBasePath, "bower_components");
            var provider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions();
            
            options.RequestPath = "/bower_components";
            options.FileProvider = provider;
            
            app.UseStaticFiles(options);
            
            return app;
        }
    }
}