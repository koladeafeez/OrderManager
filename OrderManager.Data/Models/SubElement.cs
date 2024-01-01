

namespace OrderManager.Data.Models
{
    public class SubElement : EntityBase
    {
        public Guid WindowId { get; set; }
        public int Element { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public virtual Window Window { get; set; } = null!;
    }
}
