
using OrderManager.Core.HandleWindow.CreateWindow;

namespace OrderManager.Core.HandleOrder.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public List<UpdateWindowRequest>? Windows { get; set; } = new List<UpdateWindowRequest>();

    }
}
