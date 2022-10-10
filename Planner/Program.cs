using Microsoft.EntityFrameworkCore;
using Planner.Data;

// Normaal hoef je geen Program klasse aan te maken met een Main, 
// maar dan is Program niet public, en kunnen we er niet bij
// in het Specflow test project
public static class Program
{
    public static async Task<WebApplication> MaakWebApplication(string[] args)
    {
        // Eerst worden de services geladen
        // ------------------------------------
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<PlannerContext>(o => o.UseSqlite("Data Source=db.db"));
        builder.Services.AddControllers()
                        // dit is nodig omdat we in een andere assembly testen: 
                        .AddApplicationPart(typeof(Program).Assembly)
                        .AddControllersAsServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.WebHost.UseUrls("http://localhost:5001", "https://localhost:5002");

        // Daarna wordt de middleware ingesteld
        // ------------------------------------
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        using (var context = scope.ServiceProvider.GetService<PlannerContext>()) {
            await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();
        }
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        return app;
    }
    public static async Task Main(string[] args)
    {
        (await MaakWebApplication(args)).Run();
    }
}