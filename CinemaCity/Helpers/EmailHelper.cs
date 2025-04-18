namespace CinemaCity.Helpers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public static class EmailHelper
    {
        private static readonly string apiKey = "95ade8352ba4bc502047b3ad7072ca7aac7f2248";


        public static async Task<bool> EmailExists(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.hunter.io/v2/email-verifier?email={email}&api_key={apiKey}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    string status = json["data"]?["status"]?.ToString();

                    return status == "valid";
                }
            }
            return false;
        }
        public static async Task SendEmail(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vstudio7377@gmail.com", "rdhz ufis jlpk ujsv"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("vstudio7377@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
        }
    }
}
