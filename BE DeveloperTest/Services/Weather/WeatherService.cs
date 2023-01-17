namespace BE_DeveloperTest.Services.Weather
{
    using BE_DeveloperTest.Models.Api.Weather;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.CodeAnalysis;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Umbraco.Cms.Core.Cache;

    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<OpenWeatherApiSettings> _openWeatherApiSettings;
        private readonly ILogger<WeatherService> _logger;
        private readonly AppCaches _appCaches;

        public WeatherService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherApiSettings> openWeatherApiSettings, ILogger<WeatherService> logger, AppCaches appCaches)
        {
            _httpClient = httpClientFactory.CreateClient();
            _openWeatherApiSettings = openWeatherApiSettings;
            _logger = logger;
            _appCaches = appCaches;
        }

        /// <summary>
        /// Get weather for location. Add comma after location for more accurate results.
        /// </summary>
        /// <param name="location">London,GB</param>
        /// <returns></returns>
        public async Task<WeatherViewModel> GetWeatherDataAsync(string location, int numberOfDays = 2)
        {
            //try get and return cached data first
            var cachedItem = _appCaches.RuntimeCache.GetCacheItem<WeatherData>($"WeatherResponseData-{location}");
            if (cachedItem != null && cachedItem.WeatherInfos.Any())
            {
                _logger.LogInformation($"Returning cached weather data for {location}");
                return new WeatherViewModel(cachedItem.City, GetWeatherDataDayByDay(numberOfDays, cachedItem));
            }

            var query = new Dictionary<string, string>()
            {
                ["q"] = location,
                ["appid"] = _openWeatherApiSettings.Value.OpenWeatherApiKey,
                ["units"] = "imperial"
            };

            _httpClient.BaseAddress = new Uri(_openWeatherApiSettings.Value.OpenWeatherBaseUrl);
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("/data/2.5/forecast", query));
            var result = await response.Content.ReadAsStringAsync();
            var weatherResponseData = JsonSerializer.Deserialize<WeatherData>(result);

            //Get required number of days by getting first record after midday except today.
            var weatherInfos = GetWeatherDataDayByDay(numberOfDays, weatherResponseData);

            _appCaches.RuntimeCache.GetCacheItem<WeatherData>($"WeatherResponseData-{location}", () => weatherResponseData, TimeSpan.FromHours(3));

            return new WeatherViewModel(weatherResponseData.City, weatherInfos);
        }

        private IEnumerable<WeatherInfo> GetWeatherDataDayByDay(int numberOfDays, WeatherData? weatherResponseData)
        {
            return weatherResponseData.WeatherInfos
                                .GroupBy(x => new { x.Date.Day })
                                .Select(x => x.FirstOrDefault(x => x.Date.Hour > 12) ?? x.FirstOrDefault())
                                .Take(numberOfDays);
        }
    }
}