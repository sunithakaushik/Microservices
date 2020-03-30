using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCatalogApi.Data;

namespace ProductCatalogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Breaking up build and run to seed the data, this is only for local data, not in production
            var host = CreateHostBuilder(args).Build();
            // Add the seed dependancy injection here, before we run - Module 10
            // using here is idisposable - design pattern - how an object would be destroyed
            using (var scope = host.Services.CreateScope())
            {
                var serviceProviders = scope.ServiceProvider;
                var context = serviceProviders.GetRequiredService<CatalogContext>();
                CatalogSeed.Seed(context);
            }
            // Guranteed that scope.Dispose (createscope has a dispose method) is called here, by calling in Using stmt, finalizer
            // not every object can be used inside the using stmt
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
