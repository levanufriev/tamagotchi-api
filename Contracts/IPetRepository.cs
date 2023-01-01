using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetPetsAsync(Guid farmId, PetParameters parameters, bool trackChanges);
        Task<Pet> GetPetAsync(Guid farmId, Guid id, bool trackChanges);
        void CreatePetForFarm(Guid farmId, Pet pet);
        void DeletePet(Pet pet);
    }
}
