using System.ComponentModel.DataAnnotations.Schema;

namespace TunifyPlatform.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Type { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public ICollection<User> Users { get; set; }
    }
}