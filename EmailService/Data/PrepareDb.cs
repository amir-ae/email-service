namespace EmailService.Data
{
    /// <summary>
    /// Prepares database for use.
    /// </summary>
    public static class PrepareDb
    {
        /// <summary>
        /// Ensures that the database is created and prepared to store <see cref="Mail"/> objects.
        /// </summary>
        /// <param name="app">Configures the application's request pipeline.</param>
        public static void EnsureDbCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context =
                    scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (context.Database.IsRelational() && (!context.Database.CanConnect()
                    || context.Database.GetPendingMigrations().Any()))
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
