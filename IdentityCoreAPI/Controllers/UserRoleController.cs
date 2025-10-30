using IdentityCoreAPI.DataTransferObject;
using IdentityCoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCoreAPI.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

public UserRoleController(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager; 
            _roleManager = roleManager;
        }
        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] dtoUserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(model.RoleName))
                return NotFound("Role not found");

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded) return Ok("Role assigned successfully");
            return BadRequest(result.Errors);
        }
        [HttpPost("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] dtoUserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            if (result.Succeeded) return Ok("Role removed successfully");
            return BadRequest(result.Errors);
        }
        [HttpGet("GetUserRoles/{userName}")]
        public async Task<IActionResult> GetUserRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return NotFound("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
    }// end class
}
