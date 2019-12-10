using System.Data;
using infrastructure.configurations;
using infrastructure.connections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nancy;
using Nancy.Conventions;
using Nancy.Owin;

namespace NancyBootstrapAdmin2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddSingleton<IDbConfiguration>(Configuration.GetSection("SqlLite").Get<DbConfiguration>());
            services.AddTransient<IConnection, SqlLiteConnection>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var pathConfiguration = Configuration.GetSection("Paths");
            var connection = app.ApplicationServices.GetService<IConnection>();

            string GetTables(string name)
            {
                return $"{pathConfiguration.GetValue<string>("Tables")}\\{name}.sqllite";
            }

            SqlLiteTool.CreateDatabase(pathConfiguration.GetValue<string>("Database"));
            SqlLiteTool.CreateTable(connection, GetTables("login"));
            app.UseOwin(b=>b.UseNancy());
        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.AddDirectory("Content", "Content");
        }
    }
}
