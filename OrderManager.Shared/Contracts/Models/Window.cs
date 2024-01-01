


namespace OrderManager.Shared.Contracts.Models
{
    public class WindowModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public List<SubElementModel> SubElements { get; set; } = [];
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Status { get; set; } = "Not Saved";
    }



}
