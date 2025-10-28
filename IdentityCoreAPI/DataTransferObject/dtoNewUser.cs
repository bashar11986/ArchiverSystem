using System.ComponentModel.DataAnnotations;

namespace IdentityCoreAPI.DataTransferObject
{
    public class dtoNewUser
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        public string? phoneNumber { get; set; }
    }
}
