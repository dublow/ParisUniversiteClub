using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using web.tools;

namespace NancyBootstrapAdmin2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(((context, builder) =>
                    {
                        builder.AddConfiguration(
                            ConfigurationLoader
                                .GetConfiguration(context.HostingEnvironment.EnvironmentName));
                    }))
                .Build();

            host.Run();
        }
    }
}
