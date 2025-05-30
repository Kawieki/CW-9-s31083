using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Services;

namespace PharmacyApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        
        builder.Services.AddScoped<IDbService, DbService>();
        
        var app = builder.Build();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}