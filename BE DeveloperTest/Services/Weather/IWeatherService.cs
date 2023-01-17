namespace BE_DeveloperTest.Services.Weather
{
    using BE_DeveloperTest.Models.Api.Weather;

    public interface IWeatherService
    {
        Task<WeatherViewModel> GetWeatherDataAsync(string location, int numberOfDays);
    }
}