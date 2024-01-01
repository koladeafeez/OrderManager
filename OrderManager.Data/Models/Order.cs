

namespace OrderManager.Data.Models
{
    public class Order : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public virtual ICollection<Window> Windows { get; set; } = default!;

    }
}
