using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the briefing sources
        IBriefingSource weather = new WeatherSource();
        IBriefingSource news = new NewsSource();
        IBriefingSource traffic = new TrafficSource();
        IBriefingSource trafficCamera = new TrafficCameraSource();

        // Create a cancellation token source
        CancellationTokenSource cancellationTokenSource =
            new CancellationTokenSource();

        // Cancel all operations after 1200 milliseconds
        cancellationTokenSource.CancelAfter(1200);

        // Get the cancellation token
        CancellationToken cancellationToken =
            cancellationTokenSource.Token;

        Console.WriteLine("=================================");
        Console.WriteLine("       MORNING BRIEFING");
        Console.WriteLine("=================================");
        Console.WriteLine();

        try
        {
            // Start all three normal sources
            Task<string> weatherTask =
                weather.GetInfoAsync(cancellationToken);

            Task<string> newsTask =
                news.GetInfoAsync(cancellationToken);

            Task<string> trafficTask =
                traffic.GetInfoAsync(cancellationToken);

            // Wait for all three operations
            await Task.WhenAll(
                weatherTask,
                newsTask,
                trafficTask);

            // Display the results
            Console.WriteLine(await weatherTask);
            Console.WriteLine(await newsTask);
            Console.WriteLine(await trafficTask);
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation without crashing
            Console.WriteLine("Briefing was cancelled.");
        }

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("        TRAFFIC CAMERA");
        Console.WriteLine("=================================");

        // Create a new token source for the camera
        CancellationTokenSource cameraCancellation =
            new CancellationTokenSource();

        // Cancel the camera after 1200 milliseconds
        cameraCancellation.CancelAfter(1200);

        try
        {
            // Camera needs 2000 ms, but cancellation happens
            // after 1200 ms
            string cameraInfo =
                await trafficCamera.GetInfoAsync(
                    cameraCancellation.Token);

            Console.WriteLine(cameraInfo);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine(
                "Traffic camera operation was cancelled after 1200 ms.");
        }
        catch (BriefingSourceUnavailableException ex)
        {
            Console.WriteLine("Traffic camera unavailable:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("       PROGRAM FINISHED");
        Console.WriteLine("=================================");
    }
}