using System.Collections.Generic;
using BeFaster.Core.Cqrs;

namespace BeFaster.Domain.Cqrs
{
    public class CheckoutResult : IResult
    {
        public CheckoutResult()
        {

        }
        public CheckoutResult(IDictionary<string, string> errors)
        {
            Errors = errors;
            HasErrors = errors.Count > 0 ? true : false;
        }
        public int Result { get; set; }
        public IDictionary<string, string> Errors { get; set; }
        public bool HasErrors { get; set; }
    }
}