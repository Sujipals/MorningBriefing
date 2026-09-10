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

        // Start all three operations at the same time
        Task<string> weatherTask = weather.GetInfoAsync();
        Task<string> newsTask = news.GetInfoAsync();
        Task<string> trafficTask = traffic.GetInfoAsync();

        // Wait until all three operations have finished
        await Task.WhenAll(weatherTask, newsTask, trafficTask);

        // Get the results
        string weatherInfo = await weatherTask;
        string newsInfo = await newsTask;
        string trafficInfo = await trafficTask;

        // Display the results
        Console.WriteLine(weatherInfo);
        Console.WriteLine(newsInfo);
        Console.WriteLine(trafficInfo);

        // Stop the stopwatch
        stopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Concurrent time: {stopwatch.ElapsedMilliseconds} ms");
    }
}