using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherService.Model;

namespace WeatherService.Services
{
    public class AmrutaWeatherService : IAmrutaWeatherService
    {
        public async Task<List<WeatherResponse>> GetCitiesWeatherAsync()
        {
            List<WeatherResponse> weatherResponse = new List<WeatherResponse>();
            //get the Json filepath  
            string file = @".\CityList.json";
            //deserialize JSON from file  
            string cityListContent = System.IO.File.ReadAllText(file);

            List<City> citiList = JsonConvert.DeserializeObject<List<City>>(cityListContent);
            foreach (City city in citiList)
            {
                if (!string.IsNullOrEmpty(city.ID))
                {
                    WeatherResponse rsp = await GetCityWeatherAsync(city.ID);
                    weatherResponse.Add(rsp);
                }
            }
            return weatherResponse;
        }

        public async Task<WeatherResponse> GetCityWeatherAsync(string city)
        {
            WeatherResponse weatherResponse = new WeatherResponse();
            /*Calling API http://openweathermap.org/api */
            string apiKey = "aa69195559bd4f88d79f9aadeb77a8f6";
            string apiResponse = string.Empty;
            if (!string.IsNullOrEmpty(city))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?id="+ city + "&appid="+ apiKey + "&units=metric");
                    response.EnsureSuccessStatusCode();

                     apiResponse = await response.Content.ReadAsStringAsync();
                    weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(apiResponse);

                }

                
                /*End*/

                //write string to file
                WriteToFile(city, apiResponse);

            }
            return weatherResponse;
        }

        private static void WriteToFile(string city, string apiResponse)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");

            // specify your path here or leave this blank if you just use 'bin' folder
            string path = String.Format(@".\{0}\", time);

            string filename = String.Format("{0}.txt", city);

            // This checks that path is valid, directory exists and if not - creates one:
            if (!string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(path + filename, apiResponse);
        }
      
    }
}
