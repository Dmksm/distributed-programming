using StackExchange.Redis;
using Validator.Domain.Repository;

namespace Validator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IDatabase>(cfg =>
        {
            var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            return multiplexer.GetDatabase();
        });

        // Add services to the container.
        builder.Services.AddSingleton<ITextRepository, TextRepository>();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}