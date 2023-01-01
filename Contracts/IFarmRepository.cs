using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFarmRepository
    {
        Task<IEnumerable<Farm>> GetAllFarmsAsync(bool trackChanges);
        Task<Farm> GetFarmAsync(Guid farmId, bool trackChanges);
        void CreateFarmForUser(Guid userId, Farm farm);
        Task<IEnumerable<Farm>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteFarm(Farm farm);
    }
}
