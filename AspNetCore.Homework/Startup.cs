﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using AspNetCore.Homework.Data;
using AspNetCore.Homework.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.DAL;
using Northwind.DAL.Interfaces;
using Northwind.DAL.Models;
using Swashbuckle.AspNetCore.Swagger;

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

        // This method gets called by the runtime. Use this method to Add services to the container.
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

            services.AddDbContext<UserIdentityDbContext>(
                builder => builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, NorthwindUnitOfWork>();

            services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddAzureAd(options => Configuration.Bind("AzureAd", options))
                .AddCookie();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddIdentity<IdentityUser, IdentityRole>()
                  // services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<UserIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddSingleton<IEmailSender, EmailSender>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
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

            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                logger.LogError("Custom error handler caught exception:" + ex.ToString());
                                var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                                await context.Response.WriteAsync(err).ConfigureAwait(false);
                            }
                        });
                }
            );


            app.UseMiddleware<FileCacheMiddleware>(
                Path.Combine(env.WebRootPath, Configuration.GetSection("StorageFolder")?.Value),
                int.TryParse(Configuration.GetSection("StorageCapacity")?.Value, out int capacity) ? capacity : 3);

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("images", "images/{id}",
                    defaults: new { controller = "Categories", action = "GetCategoryImageById" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
