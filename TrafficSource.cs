using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TrafficSource : IBriefingSource
{
    public async Task<string> GetInfoAsync()
    {
        await Task.Delay(800);

        return "Traffic: Moderate traffic in Copenhagen.";
    }
}