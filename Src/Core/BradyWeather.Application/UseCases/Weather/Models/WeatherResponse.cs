using System;

namespace BradyWeather.Application.UseCases.Weather.Models
{
    public class WeatherResponse
    {
        public WeatherResponse()
        {
        }

        public WeatherResponse(Forecast forecast)
        {
            WeatherText = forecast.WeatherText;
            IsDayTime = forecast.IsDayTime;
            Pressure = forecast.Pressure.Imperial.Value;
            RelativeHumidity = forecast.RelativeHumidity;
            RetrievedTime = DateTime.UtcNow;
            Temperature = forecast.Temperature.Metric.Value;
            UVIndex = forecast.UVIndex;
            WeatherIcon = forecast.WeatherIcon;
            WeatherUri = $"https://developer.accuweather.com/sites/default/files/{forecast.WeatherIcon:D2}-s.png";
            WindSpeed = forecast.Wind.Speed.Imperial.Value;
            WindDirection = forecast.Wind.Direction.English;
        }

        public string WeatherText { get; set; }

        public int WeatherIcon { get; set; }

        public string WeatherUri { get; set; }

        public bool IsDayTime { get; set; }

        public float Temperature { get; set; }

        public float RelativeHumidity { get; set; }

        public float WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public int UVIndex { get; set; }

        public float Pressure { get; set; }

        public DateTime RetrievedTime { get; set; }
    }
}
