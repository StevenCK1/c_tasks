
using GSA_Server.Core.utils;
using GSA_Server.Data.Context;

namespace GSA_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<DbHelpers>();
            builder.Services.AddScoped<CommandHelpers>();
            builder.Services.AddScoped<DatabaseQuerier>();

            builder.Services.AddDbContext<GsaserverApiContext>(ServiceLifetime.Scoped); 
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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