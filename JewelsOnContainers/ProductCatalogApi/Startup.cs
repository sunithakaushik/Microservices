using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCatalogApi.Data;

namespace ProductCatalogApi
{
    public class Startup
    {
        // Every project has a startup.cs file
        // this is a class, it is also getting injected, in the constructor goes to dbcontext
        // Dependancy injection - config file settings.json is the norm similar to app.config file which was xml file
        // json - key: value pair
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. Module 10
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // here we are adding these lines as we are running via Docker Compose
            // behind the scene here docker-compose.yml is called when running in Docker Compose 
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabasePassword"];
            var connectionString = $"Server={server};Database={database};User Id={user};Password={password}";
            services.AddDbContext<CatalogContext>(options => 
                                                    options.UseSqlServer(connectionString));
            //the below line is used when you are running in IIS express, behind the scene appsettings.json is called
            //   services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
