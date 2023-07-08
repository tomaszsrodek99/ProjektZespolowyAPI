using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Dtos;
using ProjektAPI.Models;
using ProjektAPI.Services;

namespace ProjektAPI
{
    public class Program
    {
        public IConfiguration? Configuration { get; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

            // Add services to the container.

            builder.Services.AddScoped<UserService>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserDto, User>();
            });
            builder.Services.AddAutoMapper(typeof(Program));

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

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}