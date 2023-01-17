namespace BE_DeveloperTest.Models.Api.Weather
{
    using BE_DeveloperTest.Attributes;
    using System.Text.Json.Serialization;

    public sealed class WeatherInfo
    {
        [JsonPropertyName("main")]
        public Main Main { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("dt_txt")]
        [JsonDateTimeFormat("yyyy-MM-dd HH:mm:ss")]
        public DateTime Date { get; set; }
    }
}