using System.ComponentModel.DataAnnotations.Schema;

namespace Pathao.Models
{
    public class RiderUser
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
