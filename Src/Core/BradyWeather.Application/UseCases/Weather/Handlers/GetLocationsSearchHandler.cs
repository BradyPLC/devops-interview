using System;
using System.Threading;
using System.Threading.Tasks;
using BradyWeather.Application.Interfaces;
using BradyWeather.Application.UseCases.Weather.Models;
using BradyWeather.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

namespace BradyWeather.Application.UseCases.Weather.Handlers {
    public class GetLocationsSearchHandler : IRequestHandler<GetLocationsByTextRequest, Location[]> {
        
        private readonly IWeatherClient _weatherClient;
        private readonly WeatherSettings _weatherSettings;
        private readonly ILogger<GetLocationsSearchHandler> _logger;

        public GetLocationsSearchHandler (ILogger<GetLocationsSearchHandler> logger, IWeatherClient weatherClient,  IOptionsMonitor<WeatherSettings> weatherSettings) 
        {
            _logger = logger ?? throw new ArgumentNullException (nameof (logger));
            _weatherClient = weatherClient ?? throw new ArgumentNullException(nameof(weatherClient));
            _weatherSettings = weatherSettings.CurrentValue ?? throw new ArgumentException(nameof(weatherSettings));
        }

      public async Task<Location[]> Handle(GetLocationsByTextRequest request, CancellationToken cancellationToken)
      {
          try
            {
                var response = await _weatherClient.GetLocationsByText(_weatherSettings.ApiKey, request.Search, cancellationToken);
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
            }

            return new Location[] { };
      }
    }
}