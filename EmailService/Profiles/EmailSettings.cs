namespace EmailService.Profiles
{
    /// <summary>
    /// Provides a named configuration for email server settings.
    /// </summary>
    public class EmailSettings
    {
        /// <value>The IP address or domain name for the email server.</value>
        public string? Server { get; set; }

        /// <value>The port number used by the email server.</value>
        public int Port { get; set; }

        /// <value>Email account from which email messages will be sent.</value>
        public string? Email { get; set; }

        /// <value>The password for the email account.</value>
        public string? Password { get; set; }

        /// <value>The name of the email address from which emails are sent.</value>
        public string? SenderName { get; set; }
    }
}
