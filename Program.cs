using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the three normal briefing sources
        IBriefingSource weather = new WeatherSource();
        IBriefingSource news = new NewsSource();
        IBriefingSource traffic = new TrafficSource();

        // Create the traffic camera source
        IBriefingSource trafficCamera = new TrafficCameraSource();

        // -----------------------------------------
        // SEQUENTIAL EXECUTION
        // -----------------------------------------

        Stopwatch sequentialStopwatch = Stopwatch.StartNew();

        string weatherInfo = await weather.GetInfoAsync();
        string newsInfo = await news.GetInfoAsync();
        string trafficInfo = await traffic.GetInfoAsync();

        sequentialStopwatch.Stop();

        // -----------------------------------------
        // CONCURRENT EXECUTION
        // -----------------------------------------

        Stopwatch concurrentStopwatch = Stopwatch.StartNew();

        // Start all three normal sources at the same time
        Task<string> weatherTask = weather.GetInfoAsync();
        Task<string> newsTask = news.GetInfoAsync();
        Task<string> trafficTask = traffic.GetInfoAsync();

        // Wait for all three to finish
        await Task.WhenAll(weatherTask, newsTask, trafficTask);

        string weatherInfoConcurrent = await weatherTask;
        string newsInfoConcurrent = await newsTask;
        string trafficInfoConcurrent = await trafficTask;

        concurrentStopwatch.Stop();

        // -----------------------------------------
        // DISPLAY NORMAL BRIEFING
        // -----------------------------------------

        Console.WriteLine("=================================");
        Console.WriteLine("       MORNING BRIEFING");
        Console.WriteLine("=================================");
        Console.WriteLine();

        Console.WriteLine(weatherInfoConcurrent);
        Console.WriteLine(newsInfoConcurrent);
        Console.WriteLine(trafficInfoConcurrent);

        // -----------------------------------------
        // TRAFFIC CAMERA
        // -----------------------------------------

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("        TRAFFIC CAMERA");
        Console.WriteLine("=================================");

        try
        {
            // Try to get information from the camera
            string cameraInfo = await trafficCamera.GetInfoAsync();

            // If successful, display the information
            Console.WriteLine(cameraInfo);
        }
        catch (BriefingSourceUnavailableException ex)
        {
            // Handle the camera failure without crashing
            Console.WriteLine("Traffic camera unavailable:");
            Console.WriteLine(ex.Message);
        }

        // -----------------------------------------
        // TIME COMPARISON
        // -----------------------------------------

        long savedMilliseconds =
            sequentialStopwatch.ElapsedMilliseconds -
            concurrentStopwatch.ElapsedMilliseconds;

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("          TIME COMPARISON");
        Console.WriteLine("=================================");

        Console.WriteLine(
            $"Sequential time:  {sequentialStopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine(
            $"Concurrent time:  {concurrentStopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine(
            $"Time saved:       {savedMilliseconds} ms");

        Console.WriteLine();
    }
}