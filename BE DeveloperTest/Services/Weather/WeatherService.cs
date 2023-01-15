namespace BE_DeveloperTest.Services.Weather
{
    using BE_DeveloperTest.Models.Api.Weather;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<OpenWeatherApiSettings> _openWeatherApiSettings;

        public WeatherService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherApiSettings> openWeatherApiSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _openWeatherApiSettings = openWeatherApiSettings;
        }

        /// <summary>
        /// Get weather for location. Add comma after location for more accurate results.
        /// </summary>
        /// <param name="location">London,GB</param>
        /// <returns></returns>
        public async Task<WeatherData> GetWeatherDataAsync(string location)
        {
            var weatherResponseData = new WeatherData();

            var query = new Dictionary<string, string>()
            {
                ["q"] = location,
                ["appid"] = _openWeatherApiSettings.Value.OpenWeatherApiKey
            };

            _httpClient.BaseAddress = new Uri(_openWeatherApiSettings.Value.OpenWeatherBaseUrl);
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("/data/2.5/forecast", query));
            var result = await response.Content.ReadAsStringAsync();
            weatherResponseData = JsonSerializer.Deserialize<WeatherData>(result);

            return weatherResponseData;
        }

        public Task<IEnumerable<WeatherData>> GetWeatherDatasAsync(string[] locations)
        {
            throw new NotImplementedException();
        }
    }
}