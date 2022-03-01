namespace WebApp.Controllers
{
    /// <summary>
    /// Defines the web service in single class whose methods can process HTTP requests.
    /// </summary>
    [ApiController]
    [Route("api/mails")]
    //[AutoValidateAntiforgeryToken]
    public class MailController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailController"/> class.
        /// </summary>
        /// <param name="repository"><inheritdoc cref="IRepository" path="/summary"/></param>
        /// <param name="emailSender"><inheritdoc cref="IEmailSender" path="/summary"/></param>
        /// <param name="mapper">Transforms an input object of one type into an output object of a different type.</param>
        public MailController(IRepository repository, IEmailSender emailSender, IMapper mapper)
        {
            _repository = repository;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        /// <summary>
        /// Sends email to recipients and records email data and result of sending in database.
        /// </summary>
        /// <param name="mailData">Email data recieved, expressed in the JSON format.</param>
        /// <returns>
        /// <c>201 Created</c> Success status response code when a resource is created in database,
        /// otherwise <c>500 Internal Server Error</c> response code.
        /// </returns>
        /// <remarks>
        /// Sample value of message
        /// {
        ///     "Subject": "Thank you for choosing us",
        ///     "Body": "<p>Hi John.</p><p>We just want to take this opportunity to thank you for choosing us as your company's provider.</p>",
        ///     "Recipients": ["johnsmith@example.com"]
        /// }
        /// <see cref="Mail.Result"/> is set to: <c>OK</c> if email is sent successfully, otherwise <c>Failed</c>.
        /// <see cref="Mail.FailedMessage"/> describes any errors that may have occured during email sending process.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<MailReadDto>> SendMail([FromBody] MailCreateDto mailData)
        {
            Mail m = _mapper.Map<Mail>(mailData);
            try
            {
                await _emailSender.SendEmailAsync(
                    mailData.Recipients ?? throw new ArgumentNullException(nameof(mailData.Recipients)), 
                    mailData.Subject, 
                    mailData.Body);
                m.Result = "OK";
            }
            catch (Exception ex)
            {
                m.Result = "Failed";
                m.FailedMessage = ex.Message;
            }
            _repository.CreateMail(m);
            if (await _repository.SaveChanges())
            {
                MailReadDto mailReadDto = _mapper.Map<MailReadDto>(m);
                return Created(new Uri($"{Request.Path}/{mailReadDto.Id}", UriKind.Relative), mailReadDto);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets a list of all <see cref="Mail"/> records in database, expressed in the JSON format.
        /// </summary>
        /// <returns><c>200 OK</c> success status response code with a list of all <see cref="Mail"/> records in database.</returns>
        [HttpGet]
        public async Task<ActionResult<List<MailReadDto>>> GetMails()
        {            
            List<Mail> m = await _repository.GetMails();
            return Ok(_mapper.Map<List<MailReadDto>>(m));
        }
    }
}
