namespace BE_DeveloperTest.Models.Api.Weather
{
    using System.Text.Json.Serialization;

    public sealed class City
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}