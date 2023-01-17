namespace BE_DeveloperTest.Attributes
{
    using BE_DeveloperTest.Converters;
    using System.Text.Json.Serialization;

    public sealed class JsonDateTimeFormatAttribute : JsonConverterAttribute
    {
        private readonly string format;

        public JsonDateTimeFormatAttribute(string format)
        {
            this.format = format;
        }

        public string Format => this.format;

        public override JsonConverter? CreateConverter(Type typeToConvert)
        {
            return new DateTimeFormatConverter(this.format);
        }
    }
}