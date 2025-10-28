using System.ComponentModel.DataAnnotations;

namespace IdentityCoreAPI.DataTransferObject
{
    public class dtoLogin
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }
    }
}
