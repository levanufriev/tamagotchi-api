using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public void CreateFarmForUser(Guid userId, Farm farm)
        {
            farm.Id = userId;
            Create(farm);
        }

        public void DeleteFarm(Farm farm)
        {
            Delete(farm);
        }

        public async Task<IEnumerable<Farm>> GetAllFarmsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(f => f.Name).ToListAsync();
        }

        public async Task<IEnumerable<Farm>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Farm> GetFarmAsync(Guid farmId, bool trackChanges)
        {
            return await FindByCondition(f => f.Id.Equals(farmId), trackChanges).SingleOrDefaultAsync();
        }
    }
}
