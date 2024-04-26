using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RideSimulator.Models;

public class DriverUser
{
    public Guid ID { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; } 

    public int? CurrentLogtitude { get; set; }

    public int? CurrentLattitude { get; set; }

    [Range(0, 6)]
    public double Rating { get; set; }

    public string BikeName { get; set; }

    public string BikeCC { get; set; }

    public int TotalRides { get; set; }

    public bool IsOnline { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<RideRequest> RideRequests { get; set; }

}
