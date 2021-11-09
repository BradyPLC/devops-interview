using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BradyWeather.Application.Interfaces;
using BradyWeather.Application.UseCases.Weather.Models;
using BradyWeather.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

namespace BradyWeather.Application.UseCases.Weather.Handlers
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, WeatherResponse>
    {
        private readonly ILogger<GetWeatherForecastHandler> _logger;
        private readonly IWeatherClient _weatherClient;
        private readonly WeatherSettings _weatherSettings;

        public GetWeatherForecastHandler (ILogger<GetWeatherForecastHandler> logger, IWeatherClient weatherClient,  IOptionsMonitor<WeatherSettings> weatherSettings) 
        {
            _logger = logger ?? throw new ArgumentNullException (nameof (logger));
            _weatherClient = weatherClient ?? throw new ArgumentNullException(nameof(weatherClient));
            _weatherSettings = weatherSettings.CurrentValue ?? throw new ArgumentException(nameof(weatherSettings));
        }

        public async Task<WeatherResponse> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _weatherClient.GetWeatherForecast(_weatherSettings.ApiKey, request.LocationKey, cancellationToken);
                var weather = new WeatherResponse(response.FirstOrDefault());
                return weather;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
            }

            return new WeatherResponse();
        }
    }
}