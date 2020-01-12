using System;
using System.IO;
using business;
using infrastructure.providers;
using infrastructure.repositories;
using infrastructure.tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Cryptography;
using Nancy.Owin;
using Nancy.TinyIoc;
using SQLite;
using web.auth;
using web.tools;

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

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var formAuthConfiguration = new FormsAuthenticationConfiguration(CryptographyConfiguration)
            {
                RedirectUrl = "~/Login",
                UserMapper = container.Resolve<IUserMapper>()
            };

            FormsAuthentication.Enable(pipelines, formAuthConfiguration);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            var configuration = ConfigurationLoader.GetConfiguration(_webHostEnvironment.EnvironmentName);

            var dbPath = Path.Combine(Environment.CurrentDirectory,
                $"{_webHostEnvironment.EnvironmentName.ToLower()}_puc.db");

            var db = new SQLiteConnection(dbPath);
            db.CreateTable<LoginDb>();
            db.CreateTable<RegisterDb>();

            var repository = new Repository(new Connection(dbPath));
            container.Register<IWriteRepository>(repository);
            container.Register<IReadRepository>(repository);
            container.Register<IUserMapper, UserMapper>();

            
            container
                .Register(new RegisterBusiness(container.Resolve<IWriteRepository>()));

            container.Register<IEmailSender>(new EmailSender(
                configuration.GetValue<string>("Smtp:Server"),
                configuration.GetValue<int>("Smtp:Port"),
                configuration.GetValue<string>("Smtp:FromAddress"),
                configuration.GetValue<string>("Smtp:Login"),
                configuration.GetValue<string>("Smtp:Password")));
        }

        protected override CryptographyConfiguration CryptographyConfiguration
        {
            get
            {
                return new CryptographyConfiguration(
                    new AesEncryptionProvider(new PassphraseKeyGenerator("SuperSecretPass",
                        new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })),
                    new DefaultHmacProvider(new PassphraseKeyGenerator("UberSuperSecure",
                        new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })));
            }
        }
    }
}
