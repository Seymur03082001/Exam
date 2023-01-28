using TheTown.Models.Base;

namespace TheTown.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
