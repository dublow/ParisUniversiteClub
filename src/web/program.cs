using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
                        builder.AddJsonFile($"config.{context.HostingEnvironment.EnvironmentName.ToLower()}.json");
                    }))
                .Build();

            host.Run();
        }
    }
}
