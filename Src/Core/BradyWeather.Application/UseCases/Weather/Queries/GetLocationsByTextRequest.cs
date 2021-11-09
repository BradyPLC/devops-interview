using BradyWeather.Application.UseCases.Weather.Models;
using MediatR;

namespace BradyWeather.Application.UseCases.Weather.Handlers
{
    public class GetLocationsByTextRequest : IRequest<Location[]>
    {
        public string Search { get; set; }  
    }
}