using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherService.Model;

namespace WeatherService.Services
{
    public interface IAmrutaWeatherService
    {
        Task<WeatherResponse> GetCityWeatherAsync(string city);
        Task<List<WeatherResponse>> GetCitiesWeatherAsync();
    }
}
