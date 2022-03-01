namespace EmailService.Dtos
{
    /// <summary>
    /// Data transfer object to read an email message.
    /// </summary>
    public class MailReadDto
    {
        /// <inheritdoc cref="Mail.Id" path="/value"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="MailCreateDto.Recipients" path="/value"/>
        public string[]? Recipients { get; set; }

        /// <inheritdoc cref="Mail.Subject" path="/value"/>
        public string Subject { get; set; } = string.Empty;

        /// <inheritdoc cref="Mail.Body" path="/value"/>
        public string Body { get; set; } = string.Empty;

        /// <inheritdoc cref="Mail.Result" path="/value"/>
        public string Result { get; set; } = "Draft";

        /// <inheritdoc cref="Mail.FailedMessage" path="/value"/>
        public string? FailedMessage { get; set; } = string.Empty;

        /// <inheritdoc cref="Mail.DateCreated" path="/value"/>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
