using System.ComponentModel.DataAnnotations;

namespace IdentityCoreAPI.DataTransferObject
{
    public class dtoUserRole
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string RoleName { get; set; }
    }
}
