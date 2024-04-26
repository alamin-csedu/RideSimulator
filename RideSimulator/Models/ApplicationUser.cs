using Microsoft.AspNetCore.Identity;

namespace RideSimulator.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public RiderUser RiderUser { get; set; }
        public DriverUser DriverUser { get; set; }
    }
}
