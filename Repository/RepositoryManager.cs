using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext repositoryContext;
        private IFarmRepository farmRepository;
        private IPetRepository petRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IFarmRepository Farm
        {
            get
            {
                if (farmRepository == null)
                {
                    farmRepository = new FarmRepository(repositoryContext);
                }

                return farmRepository;
            }
        }

        public IPetRepository Pet
        {
            get
            {
                if (petRepository == null)
                {
                    petRepository = new PetRepository(repositoryContext);
                }

                return petRepository;
            }
        }

        public async Task SaveAsync() => await repositoryContext.SaveChangesAsync();
    }
}
