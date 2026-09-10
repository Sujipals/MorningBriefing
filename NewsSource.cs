using System.Threading;
using System.Threading.Tasks;

public class NewsSource : IBriefingSource
{
    public async Task<string> GetInfoAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        return "News: New technology developments today.";
    }
}