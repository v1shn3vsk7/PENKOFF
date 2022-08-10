//using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace PENKOFF;

public static class Verification
{
    public static void SendEmail(string receiver, int verificationCode)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("gladys.sporer@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(receiver));
            email.Subject = "Verification";
            email.Body = new TextPart(TextFormat.Text) {Text = "Your verification code is: " + verificationCode};

            using var client = new SmtpClient();
            client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            client.Authenticate("gladys.sporer@ethereal.email", "9M7FXg7m12gPmPnasf");
            client.Send(email);
            client.Disconnect(true);
            
            /*MailMessage message = new();*/
            /*message.From = new MailAddress("penkoff.verification@yandex.com", "Penkoff Verification")*/;
            /*message.To.Add(receiver);*/
            /*message.Subject = "Verification";
            message.Body = "Your verification code is: " + verificationCode;*/

            //using SmtpClient client = new("smtp-relay.sendinblue.com");
            /*client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("penkoff.verification@yandex.com", "xD7UWFhVTc1brGSj");
            client.Port = 587;
            client.EnableSsl = true;*/
        }
        catch(Exception e)
        {

        }

    }
}