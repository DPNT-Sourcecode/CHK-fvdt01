using System.Collections.Generic;
using BeFaster.Core.Cqrs;

namespace BeFaster.Domain
{
    public class SumCommandResult : IResult
    {
        public SumCommandResult()
        {

        }
        public SumCommandResult(IDictionary<string, string> errors)
        {
            Errors = errors;
            HasErrors = errors.Count > 0 ? true : false;
        }

        public IDictionary<string, string> Errors { get; set; }
        public bool HasErrors { get; set; }
    }
}