using BeFaster.Core.Models;

namespace BeFaster.Domain
{
    public class Error : IError
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public Error() { }

        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
