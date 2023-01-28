using TheTown.Models.Base;

namespace TheTown.Models
{
    public class Product:BaseEntity
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
    }
}
