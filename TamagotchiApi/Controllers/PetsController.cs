using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TamagotchiApi.Controllers
{
    [Route("api/farms/{farmId}/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public PetsController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPetsForFarm(Guid farmId)
        {
            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null) 
            {
                return NotFound();
            }

            var pets = repository.Pet.GetPets(farmId, false);

            var petsDto = mapper.Map<IEnumerable<PetDto>>(pets);

            return Ok(petsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetPetForFarm(Guid farmId, Guid id)
        {
            var farm = repository.Farm.GetFarm(farmId, false);
            if(farm == null)
            {
                return NotFound();
            }

            var pet = repository.Pet.GetPet(farmId, id, false);

            var petDto = mapper.Map<PetDto>(pet);

            return Ok(petDto); 
        }
    }
}
