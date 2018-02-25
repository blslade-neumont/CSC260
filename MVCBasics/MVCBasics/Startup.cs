﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCBasics.Models;

namespace MVCBasics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConfig = Configuration.GetSection("DbContextSettings").Get<DbContextSettings>();
            services.AddSingleton(dbConfig);
            
            if (dbConfig.Enabled)
            {
                services.AddDbContext<SchoolDbContext>(opts => opts.UseSqlServer(dbConfig.ConnectionString));

                services.AddScoped<ICrudService<Teacher>, DbTeacherService>();
                services.AddScoped<ICrudService<Student>, DbStudentService>();
                services.AddScoped<ICrudService<Course>, DbCourseService>();
            }
            else
            {
                throw new NotSupportedException("You must start the application with the database configured and enabled");
            }

            services.AddScoped<SchoolDbInitializer>();
            services.AddScoped<StatisticsService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/{secondaryId?}");
            });
        }
    }
}
