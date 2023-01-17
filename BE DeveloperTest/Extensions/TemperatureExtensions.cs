namespace BE_DeveloperTest.Extensions
{
    public static class TemperatureExtensions
    {
        public static int ToIntCelsius(this double value)
        {
            return Convert.ToInt32((value - 32) * 5 / 9);
        }
    }
}