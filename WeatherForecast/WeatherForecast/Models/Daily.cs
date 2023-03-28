namespace WeatherForecast.Models;

public class Daily
{
    public long Dt { get; set; }
    public Temp Temp { get; set; }
    public Weather[]? Weather { get; set; }
}
