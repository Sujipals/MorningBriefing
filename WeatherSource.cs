using System.Threading;
using System.Threading.Tasks;

public class WeatherSource : IBriefingSource
{
    public async Task<string> GetInfoAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1500, cancellationToken);

        return "Weather: 18°C and partly cloudy.";
    }
}