namespace EmailService.Data
{
    /// <summary>
    /// Represents an arbitrary data store.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Creates a new <see cref="Mail"/> record in database.
        /// </summary>
        /// <param name="m">The <see cref="Mail"/> instance required to be recorded.</param>
        public void CreateMail(Mail? m);

        /// <summary>
        /// Gets a list of all <see cref="Mail"/> objects from database.
        /// </summary>
        /// <returns>List of all <see cref="Mail"/> objects in database.</returns>
        public Task<List<Mail>> GetMails();

        /// <summary>
        /// Saves changes made into the database.
        /// </summary>
        /// <returns><c>True</c> if changes are successfully saved, otherwise <c>False</c></returns>
        public Task<bool> SaveChanges();
    }
}
