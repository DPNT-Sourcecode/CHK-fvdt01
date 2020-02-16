using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Data
{
    public interface IProductRepository
    {
        Task<List<IProduct>> GetAll();
    }
}