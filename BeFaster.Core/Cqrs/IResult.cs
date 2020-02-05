

using System.Collections.Generic;

namespace BeFaster.Core.Cqrs
{
    public interface IResult
    {
        bool HasErrors { get; set; }
        IDictionary<string, string> Errors { get; set; }
    }
}
