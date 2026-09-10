using System.Threading;
using System.Threading.Tasks;

public class TrafficSource : IBriefingSource
{
    public async Task<string> GetInfoAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(800, cancellationToken);

        return "Traffic: Moderate traffic in Copenhagen.";
    }
}