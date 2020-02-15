using System.Collections.Generic;
using BeFaster.Core.Cqrs;

namespace BeFaster.Domain.Cqrs
{
    public class HelloResult : IResult
    {
        public HelloResult()
        {

        }
        public HelloResult(IDictionary<string, string> errors)
        {
            Errors = errors;
            HasErrors = errors.Count > 0 ? true : false;
        }
        public string Result { get; set; }
        public IDictionary<string, string> Errors { get; set; }
        public bool HasErrors { get; set; }
    }
}