namespace EmailService.Dtos
{
    /// <summary>
    /// Data transfer object to create an email message.
    /// </summary>
    public class MailCreateDto
    {
        /// <value>Email addresses of individuals who have opted-in to recieve this email.</value>
        [Required]
        [EmailAddressArray]
        public string[]? Recipients { get; set; }

        /// <inheritdoc cref="Mail.Subject" path="/value"/>
        [MaxLength(1000)]
        public string Subject { get; set; } = string.Empty;

        /// <inheritdoc cref="Mail.Body" path="/value"/>
        public string Body { get; set; } = string.Empty;
    }
}
