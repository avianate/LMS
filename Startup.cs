using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace LMS
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json");
            
            Configuration = builder.Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationEnvironment appEnvironment)
        {
            app.UseIISPlatformHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseFileServer();
            
            app.UseStaticFiles();
            
            app.UseBowerComponents(appEnvironment);
            
            // app.UseNodeModules();
            
            // app.useIdentity();
            
            app.UseMvc(ConfigureRoutes);
        }
        
        private void ConfigureRoutes(IRouteBuilder route)
        {
            route.MapRoute(
                "CatchAll",
                "{*url}",
                new { Controller = "Home", Action = "Index" }
            );
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
