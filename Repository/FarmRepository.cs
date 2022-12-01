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

        public IEnumerable<Farm> GetAllFarms(bool trackChanges)
        {
            return FindAll(trackChanges).ToList();
        }
    }
}
