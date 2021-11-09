using BradyWeather.Application.UseCases.Weather.Models;
using MediatR;

namespace BradyWeather.Application.UseCases.Weather.Handlers
{
    public class GetWeatherForecastRequest : IRequest<WeatherResponse>
    {
        public string LocationKey { get; set; }
    }
}