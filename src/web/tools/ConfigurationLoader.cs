using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace web.tools
{
    public static class ConfigurationLoader
    {
        public static IConfigurationRoot GetConfiguration(string environment)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();
        }
    }
}
