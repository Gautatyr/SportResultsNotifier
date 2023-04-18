using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace SportResultsNotifier;

public class Mailer
{
    private readonly string smtpAddress = ConfigurationManager.AppSettings.Get("SmtpAddress");
    private readonly int portNumber = int.Parse(ConfigurationManager.AppSettings.Get("PortNumber"));
    private readonly bool enableSSL = true;
    private readonly string senderAddress = ConfigurationManager.AppSettings.Get("SenderEmail");
    private readonly string senderPassword = ConfigurationManager.AppSettings.Get("SenderPassword");
    private readonly string receiverAdress = ConfigurationManager.AppSettings.Get("ReceiverEmail");

    public void SendEmail(string subject, string body)
    {
        using MailMessage mail = new();
        mail.From = new MailAddress(senderAddress);
        mail.To.Add(receiverAdress);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = false;

        //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));
        //--Uncomment this to send any attachment

        using SmtpClient smtp = new(smtpAddress, portNumber);
        smtp.Credentials = new NetworkCredential(senderAddress, senderPassword);
        smtp.EnableSsl = enableSSL;
        smtp.Send(mail);
    }
}