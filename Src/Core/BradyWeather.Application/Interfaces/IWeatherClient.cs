using System.Threading;
using System.Threading.Tasks;
using BradyWeather.Application.UseCases.Weather.Models;
using Refit;

namespace BradyWeather.Application.Interfaces
{
    public interface IWeatherClient
    {
        [Get("/locations/v1/cities/autocomplete?apikey={apikey}&q={search}")]
        Task<Location[]> GetLocationsByText(string apiKey, string search, CancellationToken cancellationToken);

        [Get("/currentconditions/v1/{locationKey}?apikey={apikey}&details=true")]
        Task<Forecast[]> GetWeatherForecast(string apiKey, string locationKey, CancellationToken token);
    }
}
