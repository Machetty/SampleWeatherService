using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherService.Services;

namespace TestWeatherService
{
    [TestClass]
    public class TestWeatherController
    {
        private readonly IAmrutaWeatherService _service;

        public TestWeatherController()
        {
            _service = new AmrutaWeatherService();
        }

        [TestMethod]
        public void GetCityWeatherAsync_ReturnWeather()
        {
            var result = _service.GetCityWeatherAsync("2643741");
            Assert.IsNotNull(result.Result);
        }
        [TestMethod]
        public void GetCityWeatherAsync_ReturnEmptyWeather()
        {
            var result = _service.GetCityWeatherAsync(string.Empty);
            Assert.IsTrue(result.Result.id<=0);
        }
        [TestMethod]
        public void GetCitiesWeatherAsync_ReturnCitiesWeather()
        {
            var result = _service.GetCitiesWeatherAsync();
            Assert.IsTrue(result.Result.Count>0);

        }
       
    }
}
