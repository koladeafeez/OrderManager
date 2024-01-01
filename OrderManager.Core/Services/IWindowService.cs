using OrderManager.Core.HandleOrder.CreateOrder;
using OrderManager.Shared;

namespace OrderManager.Core.Services
{
    public interface IWindowService
    {
        AppResponse GetWindowById(Guid windowId);
        AppResponse GetWindowsByOrderAsync(Guid orderId);
        AppResponse GetWindowSubElementsByWindowIdAsync(Guid orderId);
    }
}
