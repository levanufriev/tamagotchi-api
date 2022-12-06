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
        public async Task<IActionResult> GetPetsForFarm(Guid farmId)
        {
            var farm = await repository.Farm.GetFarmAsync(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with {farmId} doesn't exist in the database");
                return NotFound();
            }

            var pets = await repository.Pet.GetPetsAsync(farmId, false);

            var petsDto = mapper.Map<IEnumerable<PetDto>>(pets);

            return Ok(petsDto);
        }

        [HttpGet("{id}", Name = "GetPetForFarm")]
        public async Task<IActionResult> GetPetForFarm(Guid farmId, Guid id)
        {
            var farm = await repository.Farm.GetFarmAsync(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with {id} doesn't exist in the database");
                return NotFound();
            }

            var pet = await repository.Pet.GetPetAsync(farmId, id, false);
            if (pet == null)
            {
                logger.LogInfo($"Pet with {id} doesn't exist in the database");
                return NotFound();
            }

            var petDto = mapper.Map<PetDto>(pet);

            return Ok(petDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePetForFarm(Guid farmId, [FromBody] PetForCreationDto pet, [FromServices] IValidator<PetForCreationDto> validator)
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

            var farm = await repository.Farm.GetFarmAsync(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
                return NotFound();
            }

            var petEntity = mapper.Map<Pet>(pet);
            repository.Pet.CreatePetForFarm(farmId, petEntity);
            await repository.SaveAsync();

            var petDto = mapper.Map<PetDto>(petEntity);

            return CreatedAtRoute("GetPetForFarm", new { farmId, id = petDto.Id }, petDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetForFarm(Guid farmId, Guid id)
        {
            var farm = await repository.Farm.GetFarmAsync(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
            return NotFound();
            }

            var pet = await repository.Pet.GetPetAsync(farmId, id, false);
            if (pet == null)
            {
                logger.LogInfo($"Pet with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            repository.Pet.DeletePet(pet);
            await repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePetForFarm(Guid farmId, Guid id, [FromBody] PetForUpdateDto pet)
        {
            if (pet == null)
            {
                logger.LogError("PetForUpdateDto object sent from client is null.");
                return BadRequest("PetForUpdateDto object is null");
            }

            var farm = await repository.Farm.GetFarmAsync(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
                return NotFound();
            }

            var petEntity = repository.Pet.GetPetAsync(farmId, id, true);
            if (petEntity == null)
            {
                logger.LogInfo($"Pet with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            mapper.Map(pet, petEntity);
            await repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdatePetForFarm(Guid farmId, Guid id, [FromBody] JsonPatchDocument<PetForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var farm = await repository.Farm.GetFarmAsync(farmId, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {farmId} doesn't exist in the database.");
            return NotFound();
            }

            var petEntity = await repository.Pet.GetPetAsync(farmId, id, true);
            if (petEntity == null)
            {
                logger.LogInfo($"Pet with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var petToPatch = mapper.Map<PetForUpdateDto>(petEntity);
            patchDoc.ApplyTo(petToPatch);
            mapper.Map(petToPatch, petEntity);

            await repository.SaveAsync();
            return NoContent();
        }
    }
}
