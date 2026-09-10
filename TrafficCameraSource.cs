using System;
using System.Threading;
using System.Threading.Tasks;

public class TrafficCameraSource : IBriefingSource
{
    private static Random random = new Random();

    public async Task<string> GetInfoAsync(CancellationToken cancellationToken)
    {
        // Simulate a slow traffic camera
        await Task.Delay(2000, cancellationToken);

        // 50% chance of failure
        if (random.Next(2) == 0)
        {
            throw new BriefingSourceUnavailableException(
                "Traffic camera is currently unavailable.");
        }

        return "Traffic Camera: Traffic cameras are working normally.";
    }
}