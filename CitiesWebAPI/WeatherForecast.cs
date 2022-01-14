namespace CitiesWebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// Dit is een stuk commentaar over de Summary
        /// </summary>
        public string? Summary { get; set; }
    }
}