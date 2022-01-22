using DSU21.Data;
using DSU21.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21
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
            string connection = Configuration["ConnectionString:Default"]; //Koppling till secret.json
            connection = Configuration["ConnectionString:Develop"]; //Koppling till secret.json

            
            // kopplar DbContext mot databas. 
            //services.AddDbContext<AppDbContext>(
            //                options => options.UseNpgsql(connection,
            //                options => options.SetPostgresVersion(new Version(9, 5))));

            //services.AddDbContext<LoginDbContext>(
            //                options => options.UseNpgsql(connection,
            //                options => options.SetPostgresVersion(new Version(9, 5))));

            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection)); // koppling till annan databas
            services.AddDbContext<LoginDbContext>(options => options.UseSqlite(connection));
           
            services.AddIdentity<IdentityUser, IdentityRole>() // Skapar ett schema för identityUser och identityRole som skapar tabeller i databasen
                .AddEntityFrameworkStores<LoginDbContext>();
            services.AddScoped<IDbRepository, DbRepository>(); // Kopplar interface till repository

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddAuthorization(o => o.AddPolicy("CaptainsOnly", policy => policy.RequireClaim("Level", "5"))); // Skapar en Policy

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Kollar om du är du en autentiserad användare?
            app.UseAuthorization(); // Kollar vad du får göra.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
