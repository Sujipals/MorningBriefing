using System.Threading;
using System.Threading.Tasks;

public interface IBriefingSource
{
    Task<string> GetInfoAsync(CancellationToken cancellationToken);
}