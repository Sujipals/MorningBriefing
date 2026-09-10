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

        // -----------------------------------------
        // SEQUENTIAL EXECUTION
        // -----------------------------------------

        Stopwatch sequentialStopwatch = Stopwatch.StartNew();

        // Each operation must finish before the next one starts
        string weatherInfo = await weather.GetInfoAsync();
        string newsInfo = await news.GetInfoAsync();
        string trafficInfo = await traffic.GetInfoAsync();

        sequentialStopwatch.Stop();

        // -----------------------------------------
        // CONCURRENT EXECUTION
        // -----------------------------------------

        Stopwatch concurrentStopwatch = Stopwatch.StartNew();

        // Start all three operations immediately
        Task<string> weatherTask = weather.GetInfoAsync();
        Task<string> newsTask = news.GetInfoAsync();
        Task<string> trafficTask = traffic.GetInfoAsync();

        // Wait for all three operations to finish
        await Task.WhenAll(weatherTask, newsTask, trafficTask);

        // Get the results
        string weatherInfoConcurrent = await weatherTask;
        string newsInfoConcurrent = await newsTask;
        string trafficInfoConcurrent = await trafficTask;

        concurrentStopwatch.Stop();

        // -----------------------------------------
        // DISPLAY RESULTS
        // -----------------------------------------

        Console.WriteLine("=================================");
        Console.WriteLine("       MORNING BRIEFING");
        Console.WriteLine("=================================");
        Console.WriteLine();

        Console.WriteLine(weatherInfoConcurrent);
        Console.WriteLine(newsInfoConcurrent);
        Console.WriteLine(trafficInfoConcurrent);

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("          TIME COMPARISON");
        Console.WriteLine("=================================");

        Console.WriteLine(
            $"Sequential time:  {sequentialStopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine(
            $"Concurrent time:  {concurrentStopwatch.ElapsedMilliseconds} ms");

        // Calculate how much time was saved
        long savedMilliseconds =
            sequentialStopwatch.ElapsedMilliseconds -
            concurrentStopwatch.ElapsedMilliseconds;

        Console.WriteLine(
            $"Time saved:       {savedMilliseconds} ms");

        Console.WriteLine();
    }
}