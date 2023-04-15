using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace SportResultsNotifier;

public class Mailer
{
    string smtpAddress = ConfigurationManager.AppSettings.Get("SmtpAddress");
    int portNumber = int.Parse(ConfigurationManager.AppSettings.Get("PortNumber"));
    bool enableSSL = true;
    string senderAddress = ConfigurationManager.AppSettings.Get("SenderEmail");
    string senderPassword = ConfigurationManager.AppSettings.Get("SenderPassword");
    string receiverAdress = ConfigurationManager.AppSettings.Get("ReceiverEmail");
    string subject = "Test";
    string body = "This is a test";

    public void SendEmail()
    {
        using MailMessage mail = new();
        mail.From = new MailAddress(senderAddress);
        mail.To.Add(receiverAdress);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));
        //--Uncomment this to send any attachment  

        using SmtpClient smtp = new(smtpAddress, portNumber);
        smtp.Credentials = new NetworkCredential(senderAddress, senderPassword);
        smtp.EnableSsl = enableSSL;
        smtp.Send(mail);
    }
}
