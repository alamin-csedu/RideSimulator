namespace AuthAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
