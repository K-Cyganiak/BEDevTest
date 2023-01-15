namespace BE_DeveloperTest.Services.Weather
{
    using BE_DeveloperTest.Models.Api.Weather;

    public interface IWeatherService
    {
        Task<IEnumerable<WeatherData>> GetWeatherDatasAsync(string[] locations);

        Task<WeatherData> GetWeatherDataAsync(string location);
    }
}