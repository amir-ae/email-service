namespace EmailService.Models
{
    /// <summary>
    /// Represents an email message.
    /// </summary>
    public class Mail
    {
        /// <value>ID that uniquely identifies this <see cref="Mail"/> instance.</value>
        [Key]
        public Guid Id { get; set; }

        /// <value>Email addresses, delimited by colons, of individuals who have opted-in to recieve this email.</value>
        [Required]
        [MaxLength(1000)]
        public string? Recipients { get; set; }

        /// <value>Subject line of this email. The default is <c>Empty</c>.</value>
        [MaxLength(1000)]
        public string? Subject { get; set; } = string.Empty;

        /// <value>HTML coded text of email message. The default is <c>Empty</c>.</value>
        public string? Body { get; set; } = string.Empty;

        /// <value>Result of sending the email. The default is <c>Draft</c>.</value>
        [Required]
        [MaxLength(100)]
        public string Result { get; set; } = "Draft";

        /// <value>Description of any errors that may have occured during email sending process. The default is <c>Empty</c>.</value>
        [MaxLength(1000)]
        public string? FailedMessage { get; set; } = string.Empty;

        /// <value>Date and time of creating this email. The default is the <see cref="Mail"/> instantiation time, expressed as the Coordinated Universal Time (UTC).</value>
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
