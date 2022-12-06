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
    public class PetRepository : RepositoryBase<Pet>, IPetRepository
    {
        public PetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {                
        }

        public void CreatePetForFarm(Guid farmId, Pet pet)
        {
            pet.FarmId = farmId;
            Create(pet);
        }

        public void DeletePet(Pet pet)
        {
            Delete(pet);
        }

        public Pet GetPet(Guid farmId, Guid id, bool trackChanges)
        {
            return FindByCondition(p => p.FarmId.Equals(farmId) && p.Id.Equals(id), trackChanges).SingleOrDefault();
        }

        public IEnumerable<Pet> GetPets(Guid farmId, bool trackChanges)
        {
            return FindByCondition(p => p.FarmId.Equals(farmId), trackChanges).OrderBy(p => p.Name);
        }
    }
}
