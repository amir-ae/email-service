namespace EmailService.Data
{
    /// <summary>
    /// Provides access to the Entity Framework Core’s underlying functionality to read and write the application’s data.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class using the specified options.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <value>Provides access to <see cref="Models.Mail"/> objects in the database.</value>
        public DbSet<Mail>? Mail { get; set; }
    }
}
