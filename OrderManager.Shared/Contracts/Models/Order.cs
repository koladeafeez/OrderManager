using OrderManager.Shared.Enums;

namespace OrderManager.Shared.Contracts.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public List<Window> Windows { get; set; } = [];

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class Window
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public List<SubElement> SubElements { get; set; } = [];
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }



    }

    public class SubElement
    {
        public Guid Id { get; set; }
        public Guid WindowId { get; set; }

        public int Element { get; set; }
        public ElementType Type { get; set; }

        public double Width { get; set; }


        public double Height { get; set; }
        public int ElementNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}
