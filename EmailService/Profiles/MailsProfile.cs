namespace EmailService.Profiles
{
    /// <summary>
    /// Provides a named configuration for mail data
    /// </summary>
    public class MailsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailsProfile"/> class.
        /// </summary>
        public MailsProfile()
        {
            CreateMap<MailCreateDto, Mail>()
                .ForMember(m => m.Recipients, op => op.MapFrom(c => string.Join(";", c.Recipients ?? new string[0])));

            CreateMap<Mail, MailReadDto>()
                .ForMember(r => r.Recipients, op => op.MapFrom(m => (m.Recipients ?? string.Empty).Split(';', ',')));
        }
    }
}
