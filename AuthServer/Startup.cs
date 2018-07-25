using AuthServer.Extensions;
using BAL.Services;
using BAL.Services.Interfaces;
using DAL.Context;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using IdentityServer4;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = _configuration["connectionStrings:DefaultConnection"];
            services.AddDbContext<SignalRContext>(_ => _.UseSqlServer(connString));

            // Register Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IResourceOwnerPasswordValidator, PasswordValidator>();

            // Register Service
            services.AddScoped<IUserService, UserService>();
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Setup Identity server
            services
               .AddIdentityServer()
               .AddDeveloperSigningCredential() // This will generate a TempKey not for Production
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddInMemoryApiResources(Config.GetApiResource())
               .AddInMemoryClients(Config.GetClients())               
               .AddCustomUserStore();

            services.AddAuthentication();     

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIdentityServer();

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

            app.UseCors(option => 
                {
                    option
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
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
