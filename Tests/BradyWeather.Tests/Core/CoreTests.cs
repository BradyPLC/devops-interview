using BradyWeather.Application.Interfaces;
using BradyWeather.Application.UseCases.Weather.Handlers;
using BradyWeather.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refit;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BradyWeather.Tests
{
    [TestClass]
    public class CoreTests
    {
        IConfiguration config;

        public CoreTests()
        {
            config = InitConfiguration();
        }

        [TestMethod]
        public async Task GetLocationsSearchHandler_Handle_Test()
        {
            var weatherApiUrl = config["Web:WeatherApi:BaseAddress"];
            var weatherApiKey = config["Web:WeatherApi:ApiKey"];

            var streamingWeatherCTS = new CancellationTokenSource();
            var weatherClient = RestService.For<IWeatherClient>(weatherApiUrl);
            var logger = new Mock<ILogger<GetLocationsSearchHandler>>();
            var weatherSettings = new WeatherSettings() {
                ApiKey = weatherApiKey,
                BaseAddress = new System.Uri(weatherApiUrl)
            };
            var monitor = Mock.Of<IOptionsMonitor<WeatherSettings>>(_ => _.CurrentValue == weatherSettings);
            var handler = new GetLocationsSearchHandler(logger.Object, weatherClient, monitor);

            var weatherResponse = await handler.Handle(new GetLocationsByTextRequest() { Search = "London"}, streamingWeatherCTS.Token);

            Assert.IsNotNull(weatherResponse);
            Assert.IsTrue(weatherResponse.Length>0);
            Assert.IsTrue(weatherResponse.Select(x=>x.LocalizedName == "London").ToList().Count()>0);
        }

        [TestMethod]
        public async Task GetWeatherForecastHandler_Handle_Test()
        {
            var weatherApiUrl = config["Web:WeatherApi:BaseAddress"];
            var weatherApiKey = config["Web:WeatherApi:ApiKey"];

            var streamingWeatherCTS = new CancellationTokenSource();
            var weatherClient = RestService.For<IWeatherClient>(weatherApiUrl);
            var logger = new Mock<ILogger<GetWeatherForecastHandler>>();
            var weatherSettings = new WeatherSettings()
            {
                ApiKey = weatherApiKey,
                BaseAddress = new System.Uri(weatherApiUrl)
            };
            var monitor = Mock.Of<IOptionsMonitor<WeatherSettings>>(_ => _.CurrentValue == weatherSettings);
            var handler = new GetWeatherForecastHandler(logger.Object, weatherClient, monitor);

            var weatherResponse = await handler.Handle(new GetWeatherForecastRequest { LocationKey = "LON" }, streamingWeatherCTS.Token);

            Assert.IsNotNull(weatherResponse);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.Tests.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}
