namespace BE_DeveloperTest.Controllers.Api.Weather
{
    using BE_DeveloperTest.Models.Api.Weather;
    using BE_DeveloperTest.Services.Weather;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Weather")]
    public sealed class WeatherViewComponent : ViewComponent
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherViewComponent> _logger;
        private readonly string days = "days";
        private readonly string location = "location";

        public WeatherViewComponent(IWeatherService weatherService, ILogger<WeatherViewComponent> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(string location = "London", int numberOfDays = 2)
        {
            if (!string.IsNullOrEmpty(Request.Query["location"]))
            {
                location = Request.Query[this.location];
                _logger.LogDebug($"Current location is {location}");
            }
            if (!string.IsNullOrEmpty(Request.Query[this.days]))
            {
                if (int.TryParse(Request.Query[this.days], out int parsedDays) && parsedDays >= 2 && parsedDays <= 5)
                {
                    numberOfDays = parsedDays;
                    _logger.LogWarning($"Days query string can only be between 2-5 range and is: {parsedDays}");
                }
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync(location, numberOfDays);
                return View(weatherData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while executing GetWeatherDataAsync", ex);
            }

            return View(new WeatherViewModel());
        }
    }
}