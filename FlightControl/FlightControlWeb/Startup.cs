using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.DB;
using FlightControlWeb.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FlightControlWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


            //using (var db = new MyDBContext())
            //{
            //    db.Database.EnsureCreated();
            //    db.Database.Migrate();
            //}


        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddEntityFrameworkSqlite()
            //.AddDbContext<MyDBContext>();

            services.AddControllers();

            services.AddSingleton<IFlightsDB, MyFlightsDB>();
            services.AddSingleton<IFlightsServersDB, MyFlightsServersDB>();
            services.AddSingleton<IServersDB, MyServersDB>();
            services.AddSingleton<IFlightsModel, FlightsHandler>();
            /*services.AddSingleton(new MyServersDB("Data Source=.\\ServersDB.db;Version=3;"));
            services.AddSingleton(new MyFlightsServersDB("Data Source=.\\FlightsServersDB.db;Version=3;"));*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //allow access to files stored in wwwroot
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
