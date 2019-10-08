using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoeWebshop.Core.ApplicationServices;
using ShoeWebshop.Core.ApplicationServices.Services;
using ShoeWebshop.Core.DomainServices;
using Webshop.Infrastructure.Data;
using Webshop.Infrastructure.Data.Repositories;

namespace ShoeRestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

        }



        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddScoped<IShoeService, ShoeService>();
            services.AddScoped<IShoeRepository, ShoeRepository>();

            if (Environment.IsDevelopment())
            {
                //In-Memory Database
                services.AddDbContext<WebShopDBContext>(
                    opt => opt.UseSqlite("Data Source=shoewebshop.db"));
            }
            else
            {
                //Azure SQL Database
                services.AddDbContext<WebShopDBContext>(
                    opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<WebShopDBContext>();
                
                if (env.IsDevelopment())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    DBInitializer.SeedDB(context);
                }
                else
                {
                    //DBInitializer.SeedDB(context); //For initializing azure DB
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
