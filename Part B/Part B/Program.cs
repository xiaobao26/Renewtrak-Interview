using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Part_B.Domain.Interfaces;
using Part_B.Infrastructure;
using Part_B.Infrastructure.Repositories;
using Part_B.Services;

namespace Part_B;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Register DbContext to Container
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}