using OrderManager.Core.HandleWindow.GetWindow;
using OrderManager.Shared;

namespace OrderManager.Core.HandleOrder.GetOrder
{

    public class GetOrderOut : BaseResponseOut
    {
        public GetOrderOut(string message, GetOrderResponse response, bool success = false) : base(message, success: success)
        {
            Response = response;
        }
        public GetOrderResponse? Response { get; set; }

    }


    public class GetOrderResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string? Reference { get; set; } = string.Empty;
        public ICollection<GetWindowResponse>? Windows { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

}
