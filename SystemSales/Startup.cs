using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSales.Models;
using SystemSales.Data;
using SystemSales.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SystemSales
{
    public class Startup // Sistema de injeção de dependência
    {
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SystemSalesContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SystemSalesContext"), builder =>
                        builder.MigrationsAssembly("SystemSales")));

            services.AddScoped<SeedingService>(); // Registro de serviço no sistema de injeção dependência  da aplicação
            services.AddScoped<SellerService>();
            services.AddScoped<ServiceDepartment>();
            services.AddScoped<SalesRecordService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            // LOCALE da aplcação definido como dos EUA
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS), //locale padrão
                SupportedCultures = new List<CultureInfo> { enUS }, // locales possíveis na aplicação
                SupportedUICultures = new List<CultureInfo> { enUS }
            };
            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment()) //Serviço de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else // se o aplicativo já tiver sido publicado
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
