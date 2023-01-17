namespace BE_DeveloperTest.Converters
{
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class DateTimeFormatConverter : JsonConverter<DateTime>
    {
        private readonly string format;

        public DateTimeFormatConverter(string format)
        {
            this.format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(
                reader.GetString(),
                this.format,
                CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            ArgumentNullException.ThrowIfNull(writer, nameof(writer));

            writer.WriteStringValue(value
                .ToUniversalTime()
                .ToString(
                    this.format,
                    CultureInfo.InvariantCulture));
        }
    }
}