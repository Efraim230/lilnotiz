
using Microsoft.EntityFrameworkCore;
using WebApplication1.CONTEXT;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connectionstrings = builder.Configuration.GetConnectionString("zalanConnections");

            builder.Services.AddDbContext<BejelentkezesDbContext>(options => 
                        options.UseSqlServer(connectionstrings));

            builder.Services.AddDbContext<NotizDbContext>(options =>
                        options.UseSqlServer(connectionstrings));

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient", policy =>
                {
                    policy.WithOrigins("https://localhost:44311/") // A Blazor app URL-je
                          .AllowAnyMethod()
                          .AllowAnyOrigin()
                          .AllowAnyHeader();
                });
            });

            //app.UseCors("AllowBlazorClient");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            app.UseCors("AllowBlazorClient");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
