

using OrderManager.Core.HandleOrder.GetOrder;
using OrderManager.Shared;

namespace OrderManager.Core.HandleOrder.GetOrders
{

    public class GetOrdersOut : BaseResponseOut
    {
        public GetOrdersOut(string message, List<GetOrderResponse> response, bool success = false) : base(message, success: success)
        {
            Response = response;
        }

        public List<GetOrderResponse> Response { get; set; }
        public MetaData MetaData { get; set; }

    }

}
