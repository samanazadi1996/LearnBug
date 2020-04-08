using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


public class MailSender
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public static bool SendMail(string subject, string body, params string[] toMails)
    {
        try
        {
            var mailMsg = new MailMessage();
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.HeadersEncoding = Encoding.UTF8;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.From = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Sender = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            foreach (var mail in toMails)
            {
                mailMsg.To.Add(new MailAddress(mail));
            }
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("learnbug.com@gmail.com", "4n2h&^%$#@ui3rh834hrx348ry38ry53874");
            smtp.Send(mailMsg);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return false;
        }
    }

    public static bool SendMailByAttach(string subject, string body, string attachment, params string[] toMails)
    {
        try
        {
            var mailMsg = new MailMessage();
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.HeadersEncoding = Encoding.UTF8;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.From = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Sender = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Attachments.Add(new Attachment(attachment));
            foreach (var mail in toMails)
            {
                mailMsg.To.Add(new MailAddress(mail));
            }
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("learnbug.com@gmail.com", "4n2h&^%$#@ui3rh834hrx348ry38ry53874");
            smtp.Send(mailMsg);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return false;
        }
    }

    public static bool SendMailByAttach(string subject, string body, Attachment attachment, params string[] toMails)
    {
        try
        {
            var mailMsg = new MailMessage();
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.HeadersEncoding = Encoding.UTF8;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.From = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Sender = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Attachments.Add(attachment);
            foreach (var mail in toMails)
            {
                mailMsg.To.Add(new MailAddress(mail));
            }
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("learnbug.com@gmail.com", "4n2h&^%$#@ui3rh834hrx348ry38ry53874");
            smtp.Send(mailMsg);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return false;
        }
    }

    public static bool SendMailByAttachList(string subject, string body, string toMail, params string[] attachments)
    {
        try
        {
            var mailMsg = new MailMessage();
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.HeadersEncoding = Encoding.UTF8;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.From = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Sender = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.To.Add(new MailAddress(toMail));
            foreach (var item in attachments)
            {
                mailMsg.Attachments.Add(new Attachment(item));
            }
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("learnbug.com@gmail.com", "4n2h&^%$#@ui3rh834hrx348ry38ry53874");
            smtp.Send(mailMsg);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return false;
        }
    }

    public static bool SendMailByAttachList2(string subject, string body, string toMail, params Attachment[] attachments)
    {
        try
        {
            var mailMsg = new MailMessage();
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.HeadersEncoding = Encoding.UTF8;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.From = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.Sender = new MailAddress("learnbug.com@gmail.com", "Bug Bug", Encoding.UTF8);
            mailMsg.To.Add(new MailAddress(toMail));
            foreach (var item in attachments)
            {
                mailMsg.Attachments.Add(item);
            }
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("learnbug.com@gmail.com", "4n2h&^%$#@ui3rh834hrx348ry38ry53874");
            smtp.Send(mailMsg);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return false;
        }
    }

    public static bool SendMail(string smtp, string fromMail, string password, string fromName, string subject, string body, params string[] toMails)
    {
        try
        {
            var mailMsg = new MailMessage();
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.HeadersEncoding = Encoding.UTF8;
            mailMsg.SubjectEncoding = Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.From = new MailAddress(fromMail, fromName, Encoding.UTF8);
            mailMsg.Sender = new MailAddress(fromMail, fromName, Encoding.UTF8);
            foreach (var mail in toMails)
            {
                mailMsg.To.Add(new MailAddress(mail));
            }
            var smtpServer = new SmtpClient(smtp.Split(':')[0], Convert.ToInt32(smtp.Split(':')[1]));
            smtpServer.EnableSsl = true;
            smtpServer.Credentials = new NetworkCredential(fromMail, password);
            smtpServer.Send(mailMsg);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return false;
        }
    }

}
