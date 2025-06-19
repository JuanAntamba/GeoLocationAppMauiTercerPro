using GeoLocationAppMauiTercerPro.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static GeoLocationAppMauiTercerPro.Models.WeatherModels;

namespace GeoLocationAppMauiTercerPro.Services
{
    public class WeatherServices
    {
        public async Task<WeatherData> GetCurentLocationWeatherAsync()
        {
            GeolocationServices geolocationServices = new GeolocationServices();
            Location location = await geolocationServices.GetCurrentLocation();

            return await GetWeatherDataFromLocationAsync(location.Latitude, location.Longitude);
        }
        public async Task <WeatherData> GetWeatherDataFromLocationAsync(double latitude, double longitude)
        {
            string latitude_str = latitude.ToString().Replace(',', '.');
            string longitude_str = longitude.ToString().Replace(',', '.');

            string url = "https://api.open-meteo.com/v1/forecast?latitude="+latitude_str+"&longitude="+longitude_str+"&current=temperature_2m,relative_humidity_2m,rain`";

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resoult = await response.Content.ReadAsStringAsync();
                WeatherData data = JsonConverter.DeserializeObject<WeatherData>(result);
                return data;

            }
        }
    }
}
