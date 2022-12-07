using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TamagotchiApi.Controllers
{
    [Route("api/authentification")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = mapper.Map<User>(userForRegistration);

            var result = await userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return BadRequest(error.Description);
                }

                return BadRequest("User creation error");
            }

            await userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }
    }
}
