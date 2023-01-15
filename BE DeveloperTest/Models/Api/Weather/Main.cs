namespace BE_DeveloperTest.Models.Api.Weather
{
    using System.Text.Json.Serialization;

    public sealed class Main
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }
    }
}