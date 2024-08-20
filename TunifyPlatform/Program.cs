using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Repositories.Interfaces;
using TunifyPlatform.Repositories.Services;

namespace TunifyPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string ConnectionStringVar = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<TunifyDbContext>(opt => opt.UseSqlServer(ConnectionStringVar));

            //register the repositeries
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            builder.Services.AddScoped<ISongRepository, SongRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

            builder.Services.AddControllers();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(5002);
            });


            var app = builder.Build();

            //app.MapGet("/", () => "This is my first app");

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}