using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FarmRepository : RepositoryBase<Farm>, IFarmRepository
    {
        public FarmRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateFarm(Farm farm)
        {
            Create(farm);
        }

        public IEnumerable<Farm> GetAllFarms(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(f => f.Name).ToList();
        }

        public IEnumerable<Farm> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        }

        public Farm GetFarm(Guid farmId, bool trackChanges)
        {
            return FindByCondition(f => f.Id.Equals(farmId), trackChanges).SingleOrDefault();
        }
    }
}
