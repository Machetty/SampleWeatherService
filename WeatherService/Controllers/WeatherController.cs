using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Model;
using WeatherService.Services;

namespace WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IAmrutaWeatherService _service;

        public WeatherController(IAmrutaWeatherService service)
        {
            _service = service;
        }



        // GET: api/Weather/
        [HttpGet("Weather/{city}")]
        public async Task<ActionResult<WeatherResponse>> GetAsync(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                try
                {
                    var item = await _service.GetCityWeatherAsync(city);
                    if (item == null)
                    {
                        return NotFound();
                    }
                    return Ok(item);

                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }
            else
            {
                return BadRequest();
            }


        }

        // GET: api/GetAllCitiesWeather/
        [HttpGet]
        public async Task<ActionResult<WeatherResponse>> GetAllCitiesWeatherAsync()
        {
            try
            {
                var item = await _service.GetCitiesWeatherAsync();

                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

    }
}
