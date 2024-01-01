

using OrderManager.Core.Services;

namespace OrderManager.Core.HandleOrder.CreateOrder
{
    public class CreateOrderResponse
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
