using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Web.Identity
{
    public class ApplicationContext:IdentityDbContext<AppUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
    }
}
