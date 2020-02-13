using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Data
{
    public interface IOfferRepository
    {
        Task<List<IOffer>> GetAll();
    }
}