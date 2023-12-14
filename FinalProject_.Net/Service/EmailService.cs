using System.Net.Mail;
using System.Net;

namespace FinalProject_.Net.Service;

public class EmailService
{
	private readonly IWebHostEnvironment _env;

	public EmailService(IWebHostEnvironment env)
	{
		_env = env;
	}
	public void sendEmail(string email, string UrlContent)
	{
		string fromMail = "ninhdongnguyen@gmail.com";
		string fromPassword = "scyfyplwwpkzcyxb";
		string filePath = Path.Combine(_env.WebRootPath, "Email", "EmailTemplate.html");
		string htmlTemplate = File.ReadAllText(filePath);
		string messageContent = htmlTemplate.Replace("{{UrlConfirm}}", UrlContent);

		MailMessage message = new MailMessage();
		message.From = new MailAddress(fromMail);
		message.Subject = "Email Verification";
		message.To.Add(new MailAddress(email));
		message.Body = messageContent;
		message.IsBodyHtml = true;

		var smtpClient = new SmtpClient("smtp.gmail.com")
		{
			Port = 587,
			Credentials = new NetworkCredential(fromMail, fromPassword),
			EnableSsl = true,
		};

		smtpClient.Send(message);
	}
}