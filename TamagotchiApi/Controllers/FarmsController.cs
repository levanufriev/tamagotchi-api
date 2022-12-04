using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetFarms()
        {
            var farms = repository.Farm.GetAllFarms(false);

            var farmsDto = mapper.Map<IEnumerable<FarmDto>>(farms);

            return Ok(farmsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetFarm(Guid id)
        {
            var farm = repository.Farm.GetFarm(id, false);

            if (farm == null)
            {
                return NotFound();
            }

            var farmDto = mapper.Map<FarmDto>(farm);

            return Ok(farmDto);
        }
    }
}
