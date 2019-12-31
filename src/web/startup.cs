using System;
using System.IO;
using infrastructure.repositories;
using infrastructure.tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Owin;
using Nancy.TinyIoc;
using SQLite;

namespace web
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOwin(b=>b.UseNancy(x=>x.Bootstrapper = new CustomBootstrapper(env)));
        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomBootstrapper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.AddDirectory("Content", "Content");
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var dbPath = Path.Combine(Environment.CurrentDirectory,
                $"{_webHostEnvironment.EnvironmentName.ToLower()}_puc.db");

            var db = new SQLiteConnection(dbPath);
            db.CreateTable<LoginDb>();

            var repository = new Repository(db);
            container.Register<IWriteRepository>(repository);
            container.Register<IReadRepository>(repository);
        }
    }
}
