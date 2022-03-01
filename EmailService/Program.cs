var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AppDataConnection")
    );
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IRepository, Repository>();

//builder.Services.AddScoped<IEmailSender, ConsoleEmailSender>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.EnsureDbCreated();

app.Run();

/// <summary>
/// Gives access to the Program class
/// </summary>
public partial class Program { }