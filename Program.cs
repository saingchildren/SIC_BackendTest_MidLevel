using System.Data;
using MercuryTest.Data;
using MercuryTest.Interfaces;
using MercuryTest.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MercuryTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.OperationFilter<MercuryTest.SwaggerFilters.ACPDExampleFilter>();
            });
            var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

            builder.Services.AddScoped<IACPDService, ACPDService>();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<MercuryTest.ActionFilters.LogActionFilter>();
            });
            builder.Services.AddScoped<MercuryTest.ActionFilters.LogActionFilter>();

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
