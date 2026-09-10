using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WeatherSource : IBriefingSource
{
    public async Task<string> GetInfoAsync()
    {
        await Task.Delay(1500);

        return "Weather: 18°C and partly cloudy.";
    }
}