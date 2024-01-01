
using OrderManager.Core.HandleElement.CreateElement;

namespace OrderManager.Core.HandleWindow.CreateWindow
{
    public class CreateWindowRequest
    {
        public string Name { get; set; } = string.Empty;
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public Guid OrderId { get; set; }
        public List<CreateElementRequest>? SubElements { get; set; } = default!;

    }
}
