﻿using AutoMapper;
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
        private readonly IMapper mapper;

        public FarmsController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetFarms()
        {
            var farms = repository.Farm.GetAllFarms(false);
            
            var farmsDto = mapper.Map<IEnumerable<FarmDto>>(farms);

            return Ok(farmsDto);
        }
    }
}
