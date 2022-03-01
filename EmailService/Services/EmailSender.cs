using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EmailService.Services
{
    /// <summary>
    /// Sends an email message using the MailKit email client library.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="emailSettings"><inheritdoc cref="EmailSettings" path="/summary"/></param>
        /// <param name="environment">Describes the environment in which the application is running.</param>
        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IWebHostEnvironment environment)
        {
            _emailSettings = emailSettings.Value;
            _environment = environment;
        }

        /// <summary>
        /// <inheritdoc cref="IEmailSender" path="/summary"/>
        /// </summary>
        /// <param name="addresses"><inheritdoc cref="IEmailSender.SendEmailAsync" path="/param[@name='addresses']"/></param>
        /// <param name="subject"><inheritdoc cref="IEmailSender.SendEmailAsync" path="/param[@name='subject']"/></param>
        /// <param name="htmlMessage"><inheritdoc cref="IEmailSender.SendEmailAsync" path="/param[@name='htmlMessage']"/></param>
        /// <returns><inheritdoc cref="IEmailSender.SendEmailAsync" path="/returns"/></returns>
        public async Task SendEmailAsync(string[] addresses, string subject, string htmlMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Email));
                message.Subject = subject;

                message.Body = new TextPart("html")
                {
                    Text = htmlMessage
                };

                foreach (var address in addresses)
                {
                    message.To.Add(new MailboxAddress(null, address));
                }

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_environment.IsDevelopment())
                    {
                        await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
                    }
                    else
                    {
                        await client.ConnectAsync(_emailSettings.Server);
                    }

                    // only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
