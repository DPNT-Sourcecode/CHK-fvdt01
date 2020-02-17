using System.Threading.Tasks;

namespace BeFaster.Core.Services
{
    public interface IMessageService
    {
        Task<string> Hello(string message);
    }
}