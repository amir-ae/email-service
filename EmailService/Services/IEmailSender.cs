namespace EmailService.Services
{
    /// <summary>
    /// Used to send email messages.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <param name="addresses"><inheritdoc cref="MailCreateDto.Recipients" path="/value"/></param>
        /// <param name="subject"><inheritdoc cref="MailCreateDto.Subject" path="/value"/></param>
        /// <param name="htmlMessage"><inheritdoc cref="MailCreateDto.Body" path="/value"/></param>
        /// <returns>A task that represents the asynchronous operation of sending the email.</returns>
        Task SendEmailAsync(string[] addresses, string subject, string htmlMessage);
    }
}
