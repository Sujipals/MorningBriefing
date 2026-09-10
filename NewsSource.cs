using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NewsSource : IBriefingSource
{
    public async Task<string> GetInfoAsync()
    {
        await Task.Delay(1000);

        return "News: New technology developments today.";
    }
}