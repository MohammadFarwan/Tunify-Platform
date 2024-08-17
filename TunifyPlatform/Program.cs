using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TunifyPlatform.Data;
using TunifyPlatform.Repositories.interfaces;
using TunifyPlatform.Repositories.Services;

namespace TunifyPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            string ConnectionStringVar = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<TunifyDbContext>(optionsX => optionsX.UseSqlServer(ConnectionStringVar));

            builder.Services.AddScoped<IArtist, ArtistService>();
            builder.Services.AddScoped<IPlayList, PlaylistService>();
            builder.Services.AddScoped<ISong, SongService>();
            builder.Services.AddScoped<IUser, UserService>();

            var app = builder.Build();
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
