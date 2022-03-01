using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using EmailService.Data;

namespace WebService.Tests
{
    public class WebServiceFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                    
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest");
                });

                using (var scope = services.BuildServiceProvider().CreateScope())
                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    try
                    {
                        context.Database.EnsureCreated();
                    }
                    catch
                    {
                        throw;
                    }
                }
            });
        }
    }
}
