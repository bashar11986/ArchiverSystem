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
    public class RoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;

        }
        [HttpPost("NewRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role already exists");

            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return Ok("Role created");
        }
        [HttpDelete("DeleteRole/{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return NotFound("Role not found");

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return Ok("Role deleted successfully");
            return BadRequest(result.Errors);
        }
        
        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.Select(r => new { r.Id, r.Name }).ToList();
            return Ok(roles);
        }      

    }// end class
}
