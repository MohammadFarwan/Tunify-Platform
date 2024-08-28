using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Repositories.Interfaces;
using TunifyPlatform.Repositories.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using TunifyPlatform.Repositories.interfaces;
using TunifyPlatform.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.Options;

namespace TunifyPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string for the database
            string ConnectionStringVar = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext with SQL Server
            builder.Services.AddDbContext<TunifyDbContext>(opt => opt.UseSqlServer(ConnectionStringVar));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TunifyDbContext>();

            // Important: let the program know about IAccount & IdentityAccountService
            builder.Services.AddScoped<IAccount, IdentityAccountService>();


            // Register the repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            builder.Services.AddScoped<ISongRepository, SongRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

            builder.Services.AddScoped<JwtTokenService>();

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = JwtTokenService.ValidateToken(builder.Configuration);
                }
            );

            // Register controllers
            builder.Services.AddControllers();

            // Swagger configuration
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tunify API",
                    Version = "v1",
                    Description = "API for managing playlists, songs, and artists in the Tunify Platform"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter user token below."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });



            var app = builder.Build();

            // Add redirection from root URL to Swagger UI
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/TunifySwagger/index.html");
                }
                else
                {
                    await next();
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();


            // Enable routing
            app.UseRouting();

            // Enable Swagger
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/{documentName}/swagger.json";
            });

            // Enable Swagger UI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Tunify API v1");
                options.RoutePrefix = "TunifySwagger";  // Swagger UI at root
            });

            //For Test
            //https://localhost:7255/TunifySwagger/index.html

            // Map controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}