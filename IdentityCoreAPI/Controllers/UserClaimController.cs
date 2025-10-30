using IdentityCoreAPI.DataTransferObject;
using IdentityCoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityCoreAPI.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    [Authorize]
    public class UserClaimController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserClaimController(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;

        }
        [HttpPost("AddClaimToUser")]
        public async Task<IActionResult> AddClaimToUser([FromBody] dtoUserClaim model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return NotFound("User not found");

            var claim = new Claim(model.ClaimType, model.ClaimValue);
            var result = await _userManager.AddClaimAsync(user, claim);

            if (result.Succeeded) return Ok("Claim added successfully");
            return BadRequest(result.Errors);
        }
        [HttpPost("RemoveClaimFromUser")]
        public async Task<IActionResult> RemoveClaimFromUser([FromBody] dtoUserClaim model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return NotFound("User not found");

            var claim = new Claim(model.ClaimType, model.ClaimValue);
            var result = await _userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded) return Ok("Claim removed successfully");
            return BadRequest(result.Errors);
        }


    }
}
