
using BenefitsSystem.Repository;
using BenefitsSystem.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace BenefitsSystem.Web
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
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<DBContext>();
            services.AddScoped<IBenefitsSystemRepository, BenefitsSystemRepository>();
            services.AddSingleton<IBenefitsCalculatorService, BenefitsCalculatorService>();            
            services.AddScoped<IBenefitsSystemService, BenefitsSystemService>();
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup).Assembly);
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=BenefitsSystem}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "employeeById",
                    pattern: "{controller=BenefitsSystem}/{action=Employee}/{id}");
                endpoints.MapControllerRoute(
                    name: "dependantsByEmployeeId",
                    pattern: "{controller=BenefitsSystem}/{action=Dependants}/{id}");
               
            });
        }
    }
}
