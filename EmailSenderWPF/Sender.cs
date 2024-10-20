using System.Net.Mail;
using System.Net;

namespace EmailSenderWPF;

public class Sender
{
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
    public string Subject { get; set; }
    public string Host {  get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public string MessageBody { get; set; }

    public void SendMail(string recipientEmail)
    {
        if (string.IsNullOrEmpty(SenderName))
            SenderName = SenderEmail;

        var message = new MailMessage
        {
            From = new MailAddress(SenderEmail, SenderName),
            Subject = Subject,
            Body = MessageBody,
            IsBodyHtml = false
        };
        message.To.Add(recipientEmail);

        using (var client = new SmtpClient())
        {
            client.Host = Host;
            client.Port = Port;
            client.Credentials = new NetworkCredential(SenderEmail, Password);
            client.EnableSsl = true;

            client.Send(message);
        }

        message.Dispose();
    }
}
