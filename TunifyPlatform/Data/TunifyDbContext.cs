using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Models;

namespace TunifyPlatform.Data
{
    public class TunifyDbContext : IdentityDbContext<ApplicationUser>
    {
        public TunifyDbContext(DbContextOptions<TunifyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<PlaylistSongs> PlaylistSongs { get; set; }
        public DbSet<ArtistSongs> ArtistSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring relationships between all Entities
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SubscriptionId);

            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.User)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId);

            modelBuilder.Entity<PlaylistSongs>()
                .HasKey(ps => new { ps.PlaylistId, ps.SongId });

            modelBuilder.Entity<PlaylistSongs>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId);

            modelBuilder.Entity<PlaylistSongs>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId);

            modelBuilder.Entity<ArtistSongs>()
    .HasKey(a => new { a.ArtistId, a.SongId });

            modelBuilder.Entity<ArtistSongs>()
                .HasOne(a => a.Artist)
                .WithMany(a => a.ArtistSongs)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArtistSongs>()
                .HasOne(a => a.Song)
                .WithMany(s => s.ArtistSongs)
                .HasForeignKey(a => a.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription { Id = 1, Type = "Free", Price = 0 },
                new Subscription { Id = 2, Type = "Premium", Price = 9.99m },
                new Subscription { Id = 3, Type = "Family", Price = 14.99m }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Email = "admin@example.com", JoinDate = DateTime.Now, SubscriptionId = 1 },
                new User { UserId = 2, Username = "user1", Email = "user1@example.com", JoinDate = DateTime.Now.AddDays(-10), SubscriptionId = 1 },
                new User { UserId = 3, Username = "user2", Email = "user2@example.com", JoinDate = DateTime.Now.AddDays(-20), SubscriptionId = 2 },
                new User { UserId = 4, Username = "user3", Email = "user3@example.com", JoinDate = DateTime.Now.AddDays(-30), SubscriptionId = 2 },
                new User { UserId = 5, Username = "user4", Email = "user4@example.com", JoinDate = DateTime.Now.AddDays(-40), SubscriptionId = 1 }
            );

            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Name = "Artist 1" },
                new Artist { Id = 2, Name = "Artist 2" },
                new Artist { Id = 3, Name = "Artist 3" }
            );

            modelBuilder.Entity<Album>().HasData(
                new Album { Id = 1, Title = "Album 1" },
                new Album { Id = 2, Title = "Album 2" },
                new Album { Id = 3, Title = "Album 3" },
                new Album { Id = 4, Title = "Album 4" }
            );

            modelBuilder.Entity<Song>().HasData(
                new Song { Id = 1, Title = "Song 1", ArtistId = 1, AlbumId = 1, Duration = TimeSpan.FromMinutes(3), Genre = "Pop" },
                new Song { Id = 2, Title = "Song 2", ArtistId = 2, AlbumId = 2, Duration = TimeSpan.FromMinutes(4), Genre = "Rock" },
                new Song { Id = 3, Title = "Song 3", ArtistId = 3, AlbumId = 3, Duration = TimeSpan.FromMinutes(5), Genre = "Jazz" },
                new Song { Id = 4, Title = "Song 4", ArtistId = 1, AlbumId = 4, Duration = TimeSpan.FromMinutes(6), Genre = "Hip Hop" },
                new Song { Id = 5, Title = "Song 5", ArtistId = 2, AlbumId = 1, Duration = TimeSpan.FromMinutes(3), Genre = "Classical" }
            );

            modelBuilder.Entity<Playlist>().HasData(
                new Playlist { Id = 1, Name = "Playlist 1", UserId = 1 },
                new Playlist { Id = 2, Name = "Playlist 2", UserId = 2 },
                new Playlist { Id = 3, Name = "Playlist 3", UserId = 3 },
                new Playlist { Id = 4, Name = "Playlist 4", UserId = 4 },
                new Playlist { Id = 5, Name = "Playlist 5", UserId = 5 }
            );

            modelBuilder.Entity<ArtistSongs>().HasData(
                new ArtistSongs { ArtistId = 1, SongId = 1 },
                new ArtistSongs { ArtistId = 2, SongId = 2 },
                new ArtistSongs { ArtistId = 3, SongId = 3 },
                new ArtistSongs { ArtistId = 1, SongId = 4 },
                new ArtistSongs { ArtistId = 2, SongId = 5 }
            );

            seedRoles(modelBuilder, "Admin");
            seedRoles(modelBuilder, "User");
        }

        private void seedRoles(ModelBuilder modelBuilder, string roleName, params string[] permission)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            // add claims for the users
            // complete


            modelBuilder.Entity<IdentityRole>().HasData(role);
        }
    }
}