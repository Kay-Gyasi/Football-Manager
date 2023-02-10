using Microsoft.EntityFrameworkCore;

namespace Football_Manager.Extensions;

public static class Startup
{
    public static void BuildAndRun(this WebApplication app, WebApplicationBuilder builder)
    {
        app.RunMigrations();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    
    private static void RunMigrations(this WebApplication app)
    {
        if (Environment.CommandLine.Contains("migrations add")) return;
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        context?.Database.Migrate();
    }
}