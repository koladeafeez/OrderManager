

namespace OrderManager.Shared.Contracts.Models
{
    public class SubElementModel
    {
        public Guid Id { get; set; }
        public Guid WindowId { get; set; }
        public int Element { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
