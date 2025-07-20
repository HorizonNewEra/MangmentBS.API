using MangmentBS.API.Middlewares;
using MangmentBS.Repository.Data;
using MangmentBS.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MangmentBS.API.Helper
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> UseCustomMiddlewareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            var LoggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await AppSeeding.SeedAsync(context);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLogger<Program>().LogError(ex, $"An error occurred during database migration.");
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAngularApp");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
