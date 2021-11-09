using System;
using BradyWeather.Application.Framework;
using BradyWeather.Application.UseCases.Weather.Models;
using BradyWeather.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BradyWeather.Blazor.Server
{
    public class Startup
    {
        private readonly bool showDetailedErrors = true;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var instrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");

            services
                .AddLogging(c =>
                {
#if DEBUG
                    c.AddDebug();
                    c.AddConsole();
#else
                    if (!string.IsNullOrWhiteSpace(instrumentationKey))
                    {
                        c.AddApplicationInsights(instrumentationKey);
                    }
#endif
                });

            if (!string.IsNullOrWhiteSpace(instrumentationKey))
            {
                services.AddApplicationInsightsTelemetry(instrumentationKey);
            }

            services.Configure<WeatherSettings>(options => Configuration.GetSection("Web:WeatherApi").Bind(options));
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(i => i.DetailedErrors = showDetailedErrors);
            services.AddMediatR(typeof(Forecast));
            services.AddWeatherClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }


    }
}