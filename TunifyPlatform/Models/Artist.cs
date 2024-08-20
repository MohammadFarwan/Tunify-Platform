namespace TunifyPlatform.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<ArtistSongs> ArtistSongs { get; set; }

    }
}
