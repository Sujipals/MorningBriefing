using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BriefingSourceUnavailableException : Exception
{
    public BriefingSourceUnavailableException(string message)
        : base(message)
    {
    }
}
