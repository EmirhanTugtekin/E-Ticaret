using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Web.Models
{
    public class LoginModel
    {
        //public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
