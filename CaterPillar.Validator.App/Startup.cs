using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Services;
using CaterPillar.Validator.WebApp.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CaterPillar.Validator.App
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
            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 734003200;
            });
            services.AddControllersWithViews();
            services.AddScoped<IFileOperation, FileOperation>();
            services.AddScoped<IValidator, UnitPriceShouldNotBeGreaterThan500>();
            services.AddScoped<IValidator, UnitsSoldShouldNotBeGreaterThan9000>();
            services.AddScoped<IValidator, RegionShouldNotBeEurope>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
