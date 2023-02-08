namespace Football_Manager.Extensions;

public static class Startup
{
    public static void BuildAndStart(this WebApplication app, WebApplicationBuilder builder)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        app.RunMigrations();
    }
    
    private static void RunMigrations(this WebApplication app)
    {
        if (Environment.CommandLine.Contains("migrations add")) return;
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        context?.Database.Migrate();
    }
}