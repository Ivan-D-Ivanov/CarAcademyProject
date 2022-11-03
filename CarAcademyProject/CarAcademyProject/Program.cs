using CarAcademyProject.CommandHandlers.CarHandlers;
using CarAcademyProject.Extensions;
using CarAcademyProjectModels.ConfigurationM;
using MediatR;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace CarAcademyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddSerilog(logger);

            builder.Services.Configure<ConnectionStrings>(
                builder.Configuration.GetSection(nameof(ConnectionStrings)));

            // Add services to the container.
            builder.Services.RegisterRepositories()
                            .RegisterServices()
                            .AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(typeof(AddCarCommandHandler).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}