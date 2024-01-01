

namespace OrderManager.Data.Models
{
    public class Window : EntityBase
    {
        public Guid OrderId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public virtual ICollection<SubElement> SubElements { get; set; } = default!;
        public virtual Order Order { get; set; } = null!;


    }
}
