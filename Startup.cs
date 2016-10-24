using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SafetyDiscussionApplication.Data;
using SafetyDiscussionApplication.Models;

namespace SafetyDiscussionApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            AddTestData(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AddTestData(ApplicationDbContext context)
        {

            var testUsers = new List<User>() {
                new User() {
                    Id = 1,
                    FirstName = "Luke",
                    LastName = "Skywalker"
                },
                new User() {
                    Id = 2,
                    FirstName = "Han",
                    LastName = "Solo"
                },
                new User() {
                    Id = 3,
                    FirstName = "Chew",
                    LastName = "Bacca"
                },
                new User() {
                    Id = 4,
                    FirstName = "Obiwan",
                    LastName = "Kenobi"
                },
                new User() {
                    Id = 5,
                    FirstName = "Darth",
                    LastName = "Vader"
                }
            };            

            var testDiscussions = new List<SafetyDiscussion>() {
                new SafetyDiscussion() {
                    Id = 1,
                    ObserverUserId = 2,
                    Date = new DateTime(2016,01,16),
                    Location = "Millenium Falcon",
                    ColleagueUserId = 3,
                    Subject = "Fur",
                    Outcomes = "Discussed that Chewy is shedding fur everywhere since the weather has started wamring up. Posing an obivous trip hazard. Has agreed to get himself shaved.",
                }
            };

            context.Users.AddRange(testUsers);  
            context.SafetyDiscussions.AddRange(testDiscussions);
            context.SaveChanges();          
        }
    }
}
