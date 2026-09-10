using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the three briefing sources
        IBriefingSource weather = new WeatherSource();
        IBriefingSource news = new NewsSource();
        IBriefingSource traffic = new TrafficSource();

        // Start the stopwatch
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Wait for weather to finish
        string weatherInfo = await weather.GetInfoAsync();
        Console.WriteLine(weatherInfo);

        // Wait for news to finish
        string newsInfo = await news.GetInfoAsync();
        Console.WriteLine(newsInfo);

        // Wait for traffic to finish
        string trafficInfo = await traffic.GetInfoAsync();
        Console.WriteLine(trafficInfo);

        // Stop the stopwatch
        stopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Sequential time: {stopwatch.ElapsedMilliseconds} ms");
    }
}