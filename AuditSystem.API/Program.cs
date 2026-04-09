
using AuditSystem.Application.Features.Enrollments.Commands.Enroll_Course;
using AuditSystem.Application.Features.Enrollments.Queries;
using AuditSystem.Infrastructure;
using Microsoft.OpenApi;

namespace AuditSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Pass IConfiguration to InfraStructure
            builder.Services.AddInfrastructure(builder.Configuration);

            // Add services to the container.
            builder.Services.AddScoped<EnrollCourseHandler>();
            builder.Services.AddScoped<GetEnrollmentsHandler>();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Audit System API",
                    Version = "v1",
                    Description = "Event-Driven Audit Logging System"
                });
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audit System API V1");
                    c.RoutePrefix = "swagger";
                });

                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
