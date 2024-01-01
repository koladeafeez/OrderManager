using OrderManager.Core.HandleWindow.CreateWindow;

namespace OrderManager.Core.HandleOrder.CreateOrder
{


    public class CreateOrderRequest
    {

        public string Name { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public List<CreateWindowRequest>? Windows { get; set; } = default!;

    }

}
