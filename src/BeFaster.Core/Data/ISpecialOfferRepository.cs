using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Data
{
    public interface ISpecialOfferRepository
    {
        Task<List<ISpecialOffer>> GetAll();
    }
}