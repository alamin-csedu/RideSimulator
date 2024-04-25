using Microsoft.AspNetCore.Identity;

namespace Pathao.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public RiderUser RiderUser { get; set; }
        public DriverUser DriverUser { get; set; }
    }
}
