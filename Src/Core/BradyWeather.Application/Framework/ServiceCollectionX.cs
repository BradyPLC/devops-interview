using Microsoft.Extensions.DependencyInjection;
using BradyWeather.Application.Interfaces;
using BradyWeather.Common;
using Microsoft.Extensions.Options;
using Refit;

namespace BradyWeather.Application.Framework
{
    public static class ServiceCollectionX
    {
        private const string Json = "application/json";
        private const string Accept = "Accept";

        public static void AddWeatherClient(this IServiceCollection services)
        {
            services.AddRefitClient<IWeatherClient>()
                .ConfigureHttpClient((sp, options) =>
                {
                    var httpClientOptions = sp.GetRequiredService<IOptionsMonitor<WeatherSettings>>().CurrentValue;
                    options.BaseAddress = httpClientOptions.BaseAddress;
                    options.DefaultRequestHeaders.Add(Accept, Json);
                });
        }
    }
}
