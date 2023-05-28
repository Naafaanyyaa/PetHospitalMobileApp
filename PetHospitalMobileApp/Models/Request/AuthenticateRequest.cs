using System.ComponentModel.DataAnnotations;

namespace PetHospital.Business.Models.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public string userName { set; get; } = string.Empty;
        [Required]
        public string password { set; get; } = string.Empty;
    }
}
