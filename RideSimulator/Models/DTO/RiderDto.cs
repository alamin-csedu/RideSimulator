using System.ComponentModel.DataAnnotations;

namespace Pathao.Models.DTO
{
    public class RiderDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Phone Number must be in format 01XXXXXXXXX")]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }
    }
}
