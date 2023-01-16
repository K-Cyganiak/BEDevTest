namespace BE_DeveloperTest.Controllers.Api.Weather
{
    using BE_DeveloperTest.Models.Api.Weather;
    using BE_DeveloperTest.Services.Weather;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Weather")]
    public sealed class WeatherViewComponent : ViewComponent
    {
        private readonly IWeatherService _weatherService;

        public WeatherViewComponent(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string location = "London")
        {
            if (!string.IsNullOrEmpty(Request.Query["location"]))
            {
                location = Request.Query["location"];
            }

            var weatherData = await _weatherService.GetWeatherDataAsync(location);

            return View(weatherData);
        }

        private async Task<string> GetWeatherData(string location = "London")
        {
            var locations = location.ToLower().Split(',');
            var weatherData = await _weatherService.GetWeatherDataAsync(location);

            return location;
        }
    }
}