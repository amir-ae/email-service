namespace EmailService.Data
{
    /// <summary>
    /// Repository implementation using Entity Framework Core.
    /// </summary>
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context"><inheritdoc cref="AppDbContext" path="/summary"/></param>
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <inheritdoc cref="IRepository.CreateMail(Mail?)" path="/summary"/>
        /// </summary>
        /// <param name="m"><inheritdoc cref="IRepository.CreateMail(Mail?)" path="/param[@name='m']"/></param>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="Mail"/> value is <c>Null</c>.</exception>
        /// <exception cref="InvalidOperationException">Thrown if database is not prepared or not connected.</exception>
        public void CreateMail(Mail? m)
        {
            if (m == null)
            {
                throw new ArgumentNullException(nameof(m));
            }
            if (_context.Mail == null)
            {
                throw new InvalidOperationException();
            }
            _context.Mail.Add(m);
        }

        /// <summary>
        /// <inheritdoc cref="IRepository.GetMails" path="/summary"/>
        /// </summary>
        /// <returns><inheritdoc cref="IRepository.GetMails" path="/returns"/></returns>
        /// <exception cref="InvalidOperationException">Thrown if database is not prepared or not connected.</exception>
        public async Task<List<Mail>> GetMails()
        {
            if (_context.Mail == null)
            {
                throw new InvalidOperationException();
            }
            return await _context.Mail.ToListAsync();
        }

        /// <summary>
        /// <inheritdoc cref="IRepository.SaveChanges" path="/summary"/>
        /// </summary>
        /// <returns><inheritdoc cref="IRepository.SaveChanges" path="/returns"/></returns>
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
