﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.DAL;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;

namespace AspNetCore.Homework
{
    public class Startup
    {
        private ILogger<Startup> logger;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<NorthwindContext>(
                builder => builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, NorthwindUnitOfWork>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/homework-{Date}.txt");

            logger = loggerFactory.CreateLogger<Startup>();

            logger.LogInformation($"Application folder:{env.WebRootPath}");

            logger.LogInformation(
                $"Configuration value of M:{(int.TryParse(Configuration.GetSection("M")?.Value, out int max) ? max : -1)}");

            logger.LogInformation($"Connection string:{Configuration.GetConnectionString("DefaultConnection")}");

          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.Use(next =>
            {
                return async context =>
                {
                    try
                    {
                        await next(context);
                    }
                    catch (Exception e)
                    {
                        logger.LogError("Custom error handler cought exception"+e.ToString());
                        throw;
                    }
                };
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
