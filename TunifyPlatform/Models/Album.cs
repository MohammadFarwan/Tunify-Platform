namespace TunifyPlatform.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
