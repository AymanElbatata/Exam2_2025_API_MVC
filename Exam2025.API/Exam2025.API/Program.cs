
using Exam2025.API.Helpers;
using Exam2025.BLL.Interfaces;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Exam2025.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ExamDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyOrigin().AllowAnyOrigin();
                });
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error != null)
                    {
                        var ex = exceptionHandlerPathFeature.Error;

                        // Log
                        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Global exception caught at {Path}", exceptionHandlerPathFeature.Path);

                        // Save to DB
                        using var scope = context.RequestServices.CreateScope();
                        var repo = scope.ServiceProvider.GetRequiredService<IAppErrorTBLRepository>();
                        repo.Add(new AppErrorTBL
                        {
                            Message = ex.Message,
                            StackTrace = ex.StackTrace ?? "",
                            Controller = exceptionHandlerPathFeature.Path ?? "",
                            Action = "" // optional
                        });
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
                    {
                        error = "An unexpected error occurred. Please try again later."
                    }));
                });
            });

            SeedInitialData.SeedData(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapControllerRoute(
            //    name: "areas",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
