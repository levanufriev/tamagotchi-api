using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TamagotchiApi.ModelBinders;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TamagotchiApi.Controllers
{
    [Route("api/farms")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public FarmsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetFarms()
        {
            var farms = await repository.Farm.GetAllFarmsAsync(false);

            var farmsDto = mapper.Map<IEnumerable<FarmDto>>(farms);

            return Ok(farmsDto);
        }

        [HttpGet("{id}", Name = "FarmById")]
        public async Task<IActionResult> GetFarm(Guid id)
        {
            var farm = await repository.Farm.GetFarmAsync(id, false);

            if (farm == null)
            {
                logger.LogInfo($"Farm with {id} doesn't exist in the database");
                return NotFound();
            }

            var farmDto = mapper.Map<FarmDto>(farm);

            return Ok(farmDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromBody] FarmForCreationDto farm)
        {
            if(farm == null)
            {
                logger.LogError("FarmForCreationDto object sent from client is null.");
                return BadRequest("FarmForCreationDto object is null");
            }

            var farmEntity = mapper.Map<Farm>(farm);
            repository.Farm.CreateFarm(farmEntity);
            await repository.SaveAsync();

            var farmDto = mapper.Map<FarmDto>(farmEntity);

            return CreatedAtRoute("FarmById", new { id = farmDto.Id }, farmDto);
        }

        [HttpGet("collection/({ids})", Name = "FarmCollection")]
        public async Task<IActionResult> GetFarmCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var farmEntities = await repository.Farm.GetByIdsAsync(ids, false);
            if (ids.Count() != farmEntities.Count())
            {
                logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var farmsDto = mapper.Map<IEnumerable<FarmDto>>(farmEntities);
            return Ok(farmsDto);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateFarmCollection([FromBody] IEnumerable<FarmForCreationDto> farmCollection)
        {
            if (farmCollection == null)
            {
                logger.LogError("Farm collection sent from client is null.");
                return BadRequest("Farm collection is null");
            }

            var farmEntities = mapper.Map<IEnumerable<Farm>>(farmCollection);
            foreach (var farm in farmEntities)
            {
                repository.Farm.CreateFarm(farm);
            }

            await repository.SaveAsync();

            var farmsDto = mapper.Map<IEnumerable<FarmDto>>(farmEntities);

            var ids = string.Join(",", farmsDto.Select(f => f.Id));
            return CreatedAtRoute("FarmCollection", new { ids }, farmsDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarm(Guid id)
        {
            var farm = await repository.Farm.GetFarmAsync(id, false);
            if (farm == null)
            {
                logger.LogInfo($"Farm with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            repository.Farm.DeleteFarm(farm);
            await repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarm(Guid id, [FromBody] FarmForUpdateDto farm)
        {
            if (farm == null)
            {
                logger.LogError("FarmForUpdateDto object sent from client is null.");
                return BadRequest("FarmForUpdateDto object is null");
            }

            var farmEntity = await repository.Farm.GetFarmAsync(id, true);
            if (farmEntity == null)
            {
                logger.LogInfo($"Farm with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            mapper.Map(farm, farmEntity);
            await repository.SaveAsync();
            return NoContent();
        }

    }
}
