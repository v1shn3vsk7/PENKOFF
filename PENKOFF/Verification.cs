using System.Net;
using System.Net.Mail;

namespace PENKOFF;

public static class Verification
{
    public static void SendEmail(string receiver, int verificationCode)
    {
        try
        {
            MailMessage message = new();
            message.From = new MailAddress("penkoff.verification@yandex.com", "Penkoff Verification");
            message.To.Add(receiver);
            message.Subject = "Verification";
            message.Body = "Your verification code is: " + verificationCode;

            using SmtpClient client = new("smtp-relay.sendinblue.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("penkoff.verification@yandex.com", "xD7UWFhVTc1brGSj");
            client.Port = 587;
            client.EnableSsl = true;

            client.Send(message);
        }
        catch(Exception e)
        {

        }

    }
}