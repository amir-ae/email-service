namespace EmailService.Services
{
    /// <summary>
    /// Writes the text representation of an email message to the standard output stream.
    /// </summary>
    public class ConsoleEmailSender : IEmailSender
    {
        /// <summary>
        /// <inheritdoc cref="IEmailSender" path="/summary"/>
        /// </summary>
        /// <param name="addresses"><inheritdoc cref="IEmailSender.SendEmailAsync" path="/param[@name='addresses']"/></param>
        /// <param name="subject"><inheritdoc cref="IEmailSender.SendEmailAsync" path="/param[@name='subject']"/></param>
        /// <param name="htmlMessage"><inheritdoc cref="IEmailSender.SendEmailAsync" path="/param[@name='htmlMessage']"/></param>
        /// <returns><inheritdoc cref="IEmailSender.SendEmailAsync" path="/returns"/></returns>
        public Task SendEmailAsync(string[] addresses,
            string subject, string htmlMessage)
        {
            Console.WriteLine("---New Email----");
            Console.WriteLine($"To: {string.Join(";", addresses)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine(HttpUtility.HtmlDecode(htmlMessage));
            Console.WriteLine("-------");
            return Task.CompletedTask;
        }
    }
}
