using System.ComponentModel.DataAnnotations;

namespace IdentityCoreAPI.DataTransferObject
{
    public class dtoUserClaim
    {
        [Required]
        public string UserName { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
