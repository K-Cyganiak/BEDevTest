namespace BE_DeveloperTest.Models.Api.Weather
{
    using System.Text.Json.Serialization;

    public sealed class WeatherData
    {
        [JsonPropertyName("cod")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int Code { get; set; }

        /// <summary>
        /// For some weird reason message can be either int or string.
        /// System.Text.Json does not handle these conversions nicely.
        /// Easier approach would be to to use Newtonsoft.Json
        /// </summary>
        [JsonPropertyName("message")]
        public object _Message
        {
            get
            {
                if (int.TryParse(Message, out var intValue)) return intValue;
                return this.Message;
            }
            set { this.Message = value.ToString(); }
        }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonPropertyName("list")]
        public List<WeatherInfo> WeatherInfos { get; set; }

        [JsonPropertyName("city")]
        public City City { get; set; }
    }
}