using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Part_B.Domain.Interfaces;
using Part_B.Infrastructure;
using Part_B.Infrastructure.Repositories;
using Part_B.Middlewares;
using Part_B.Services;

namespace Part_B;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        if (builder.Environment.IsProduction())
        {
            Directory.CreateDirectory("/home/data");
        }
        
        // CORS
        const string CorsPolicy = "_swa";
        var swaOrigin = builder.Configuration["AllowedFrontend"];
        builder.Services.AddCors(
            o => o.AddPolicy(CorsPolicy, 
            p => p.WithOrigins(swaOrigin).AllowAnyMethod().AllowAnyHeader()
            ));
        
        // Register DbContext to Container
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        // Register Controllers
        builder.Services.AddControllers();
        
        // Add services to the container.
        builder.Services.AddScoped<IGlossaryRepository, GlossaryRepository>();
        builder.Services.AddScoped<IGlossaryService, GlossaryService>();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Glossary API", Version = "v1" });
        });

        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseCors(CorsPolicy);
        app.UseAuthorization();
        app.MapControllers();
        
        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }
        app.Run();
    }
}