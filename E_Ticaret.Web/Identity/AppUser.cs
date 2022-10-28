using Microsoft.AspNetCore.Identity;

namespace E_Ticaret.Web.Identity
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
