using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPetRepository
    {
        IEnumerable<Pet> GetPets(Guid farmId, bool trackChanges);
        Pet GetPet(Guid farmId, Guid id, bool trackChanges);
        void CreatePetForFarm(Guid farmId, Pet pet);
        void DeletePet(Pet pet);
    }
}
