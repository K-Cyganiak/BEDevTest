namespace BE_DeveloperTest.Models.Api.Weather
{
    using BE_DeveloperTest.Extensions;
    using Humanizer;
    using System.Linq;

    public sealed class WeatherViewModel
    {
        public string City { get; set; }
        public string Country { get; set; }
        public List<WeatherDetails> WeatherDetails { get; set; } = new List<WeatherDetails>();

        public WeatherViewModel(City city, IEnumerable<WeatherInfo> weatherInfos)
        {
            City = city.Name;
            Country = city.Country;

            foreach (var info in weatherInfos)
            {
                var weatherDeatils = new WeatherDetails
                {
                    Day = info.Date.Date == DateTime.Today ? $"Today {info.Date.Day.Ordinalize()}" : $"{info.Date.DayOfWeek} {info.Date.Day.Ordinalize()}",
                    Weather = info.Weather.FirstOrDefault().Description,
                    WeatherCssClass = info.Weather.FirstOrDefault().Description.ToLower().Replace(" ", ""),
                    WeatherDescription = info.Weather.FirstOrDefault().Description,
                    TemperatureFahrenheit = Convert.ToInt32(info.Main.Temperature),
                    TemperatureCelsius = info.Main.Temperature.ToIntCelsius(),
                    Icon = info.Weather.FirstOrDefault().Icon,
                };

                WeatherDetails.Add(weatherDeatils);
            }
        }

        public WeatherViewModel()
        {
        }
    }

    public sealed class WeatherDetails
    {
        public string Day { get; set; }
        public string Weather { get; set; }
        public int TemperatureFahrenheit { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Icon { get; set; }
        public string WeatherCssClass { get; set; }
        public string WeatherDescription { get; set; }
    }
}