using BeFaster.Core.Data;
using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Services
{
    public interface ICartService
    {
        Task<int> Checkout(string skus);
        int Calculate(ICart cart);        
        int CalculateWithoutOffers(ICart cart);    
    }
}