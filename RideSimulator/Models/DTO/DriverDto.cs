using System.ComponentModel.DataAnnotations;

namespace RideSimulator.Models.DTO;

public class DriverDto
{
    [Required]
    [Phone]
    [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Phone Number must be in format 01XXXXXXXXX")]
    public string PhoneNumber { get; set;}

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string BikeName { get; set; }

    public string BikeCC { get; set; }
}
