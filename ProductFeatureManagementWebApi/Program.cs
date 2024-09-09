using Microsoft.EntityFrameworkCore;
using ProductFeatureManagementWebApi.Models;
using ProductFeatureManagementWebApi.Services;
using Serilog;

namespace ProductFeatureManagementWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()  // Log to the console
                            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)  // Log to a file, rolling daily
                            .Enrich.FromLogContext()
                            .MinimumLevel.Debug() // Set the minimum logging level
                            .CreateLogger();

            // Add services to the container.
            builder.Services.AddControllers();

            // Register Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddDbContext<ProductFeatureMgmtDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            // Add repositories with scoped lifetime
            builder.Services.AddScoped<IStatusRepository, StatusRepository>();
            builder.Services.AddScoped<IComplexityRepository, ComplexityRepository>();
            builder.Services.AddScoped<IFeaturesRepository, FeaturesRepository>();

            // Add services with scoped lifetime
            builder.Services.AddScoped<IStatusService, StatusService>();
            builder.Services.AddScoped<IComplexityService, ComplexityService>();
            builder.Services.AddScoped<IFeaturesService, FeaturesService>();


            var app = builder.Build();

            // Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect the root URL to Swagger UI
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                }
                else
                {
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowAngularOrigins");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}