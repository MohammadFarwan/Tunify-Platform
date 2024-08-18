namespace TunifyPlatform.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ArtistId { get; set; } // Foreign Key
        public Artist Artist { get; set; } // For Foreign Key
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<PlaylistSongs> PlaylistSongs { get; set; } // Navigation property
    }
}
