
using System.Net;
using System.Text;

namespace RideSimulator.Service
{
    public class NotificationService : INotificationService
    {
        public async Task<bool> SendOTPAsync(string recipient, string otp)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String api_key = "1UdUQPcwTjoYEljcwM5L"; //Your api_key
                String senderid = "8809617618250"; //Your Sender ID
                String number = recipient;
                String message = System.Uri.EscapeUriString(String.Format("Your OTP for login is {0}",otp)); 
                String url = "http://bulksmsbd.net/api/smsapi?api_key=" + api_key + "&senderid=" + senderid + "&number=" + number + "&message=" + message;
                request = WebRequest.Create(url);

                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

        }
    }
}
