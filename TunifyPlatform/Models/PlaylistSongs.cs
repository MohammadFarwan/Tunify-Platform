namespace TunifyPlatform.Models
{
    public class PlaylistSongs
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public int SongId { get; set; } // Foreign key
        public Song Song { get; set; } // Navigation property
    }
}
