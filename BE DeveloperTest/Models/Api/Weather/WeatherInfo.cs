namespace BE_DeveloperTest.Models.Api.Weather
{
    using System.Text.Json.Serialization;

    public sealed class WeatherInfo
    {
        [JsonPropertyName("dt")]
        public int Date { get; set; }

        [JsonPropertyName("main")]
        public Main Main { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("dt_txt")]
        public string DateText { get; set; }
    }
}