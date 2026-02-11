using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Data;
using Project.Model.Domain;

namespace Project.Service
{
    public class EmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public EmailService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> SendEmailsAsync(string recipientEmail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(recipientEmail))
                    throw new ArgumentException("❌ Recipient email cannot be null or empty.");

                // ✅ Fetch ALL Email Templates
                var mailTemplates = await _context.tblMailTemp
                    .FromSqlRaw("SELECT MBODY, SUBJECT FROM dbo.GET_MailTemplate()")
                    .AsNoTracking()
                    .Select(t => new TblMailTemp { MBODY = t.MBODY, SUBJECT = t.SUBJECT })
                    .ToListAsync();

                if (mailTemplates == null || mailTemplates.Count == 0)
                    throw new Exception("❌ No Email Templates Found!");

                // ✅ SMTP Configuration
                var emailSettings = _configuration.GetSection("SMTP");
                string fromEmail = emailSettings["Username"];
                string password = emailSettings["Password"];
                string host = emailSettings["Host"];
                int port = int.Parse(emailSettings["Port"]);

                if (string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("❌ SMTP credentials cannot be null or empty.");

                using (SmtpClient client = new SmtpClient(host, port))
                {
                    client.Credentials = new NetworkCredential(fromEmail, password);
                    client.EnableSsl = true;

                    // ✅ Loop Through Each Email Template & Send
                    foreach (var mailTemplate in mailTemplates)
                    {
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress(fromEmail),
                            Subject = mailTemplate.SUBJECT,
                            Body = mailTemplate.MBODY,
                            IsBodyHtml = true
                        };
                        mail.To.Add(recipientEmail);

                        await client.SendMailAsync(mail);
                        Console.WriteLine($"✅ Mail Sent: {mailTemplate.SUBJECT}");
                    }
                }

                return "✅ All Emails Sent Successfully!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return $"❌ Error: {ex.Message}";
            }
        }
    }
}
