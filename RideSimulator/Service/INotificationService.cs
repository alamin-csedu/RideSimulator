namespace RideSimulator.Service
{
    public interface INotificationService
    {
        public Task<bool> SendOTPAsync(string recipient, string otp);
    }
}
