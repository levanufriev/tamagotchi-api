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
        IEnumerable<Farm> GetAllFarms(bool trackChanges);
        Farm GetFarm(Guid farmId, bool trackChanges);
        void CreateFarm(Farm farm);
        IEnumerable<Farm> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
