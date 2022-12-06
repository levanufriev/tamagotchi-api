using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace TamagotchiApi.Controllers
{
    [Route("api/farms/{farmId}/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public PetsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPetsForFarm(Guid farmId)
        {
            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with {farmId} doesn't exist in the database");
                return NotFound();
            }

            var pets = repository.Pet.GetPets(farmId, false);

            var petsDto = mapper.Map<IEnumerable<PetDto>>(pets);

            return Ok(petsDto);
        }

        [HttpGet("{id}", Name = "GetPetForFarm")]
        public IActionResult GetPetForFarm(Guid farmId, Guid id)
        {
            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with {id} doesn't exist in the database");
                return NotFound();
            }

            var pet = repository.Pet.GetPet(farmId, id, false);
            if (pet == null)
            {
                logger.LogInfo($"Pet with {id} doesn't exist in the database");
                return NotFound();
            }

            var petDto = mapper.Map<PetDto>(pet);

            return Ok(petDto);
        }

        [HttpPost]
        public IActionResult CreatePetForFarm(Guid farmId, [FromBody] PetForCreationDto pet, [FromServices] IValidator<PetForCreationDto> validator)
        {
            if (pet == null)
            {
                logger.LogError("PetForCreationDto object sent from client is null.");
                return BadRequest("PetForCreationDto object is null");
            }

            if (!validator.Validate(pet).IsValid)
            {
                logger.LogError("Invalid model state for the PetForCreationDto object");
                return BadRequest("PetForCreationDto object is invalid");
            }

            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
                return NotFound();
            }

            var petEntity = mapper.Map<Pet>(pet);
            repository.Pet.CreatePetForFarm(farmId, petEntity);
            repository.Save();

            var petDto = mapper.Map<PetDto>(petEntity);

            return CreatedAtRoute("GetPetForFarm", new { farmId, id = petDto.Id }, petDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePetForFarm(Guid farmId, Guid id)
        {
            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
            return NotFound();
            }

            var pet = repository.Pet.GetPet(farmId, id, false);
            if (pet == null)
            {
                logger.LogInfo($"Pet with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            repository.Pet.DeletePet(pet);
            repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePetForFarm(Guid farmId, Guid id, [FromBody] PetForUpdateDto pet)
        {
            if (pet == null)
            {
                logger.LogError("PetForUpdateDto object sent from client is null.");
                return BadRequest("PetForUpdateDto object is null");
            }

            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
            return NotFound();
            }

            var petEntity = repository.Pet.GetPet(farmId, id, true);
            if (petEntity == null)
            {
                logger.LogInfo($"Pet with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            mapper.Map(pet, petEntity);
            repository.Save();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePetForFarm(Guid farmId, Guid id, [FromBody] JsonPatchDocument<PetForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var farm = repository.Farm.GetFarm(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
            return NotFound();
            }

            var petEntity = repository.Pet.GetPet(farmId, id, true);
            if (petEntity == null)
            {
                logger.LogInfo($"Pet with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var petToPatch = mapper.Map<PetForUpdateDto>(petEntity);
            patchDoc.ApplyTo(petToPatch);
            mapper.Map(petToPatch, petEntity);

            repository.Save();
            return NoContent();
        }
    }
}
