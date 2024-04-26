using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RideSimulator.Models
{
    public class RiderUser
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<RideRequest> RideRequests { get; set; }
    }
}
