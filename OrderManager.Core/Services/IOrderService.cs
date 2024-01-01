using OrderManager.Core.HandleOrder.CreateOrder;
using OrderManager.Core.HandleOrder.UpdateOrder;
using OrderManager.Core.HandleWindow.CreateWindow;
using OrderManager.Data.Models;
using OrderManager.Shared;

namespace OrderManager.Core.Services
{
    public interface IOrderService : IBaseService<Order, List<CreateOrderRequest>>
    {
        Task<AppResponse> GetOrderAsync(Guid orderId);
        Task<AppResponse> GetOrdersAsync();

        Task<AppResponse> DeleteOrder(Guid orderId);
        AppResponse CreateOrder(CreateOrderRequest order);
        Task<AppResponse> CreateWindow(CreateWindowRequest newWindow);
        Task<AppResponse> UpdateOrder(Guid id, UpdateOrderRequest updatedOrder);
        Task<AppResponse> UpdateWindow( Guid id, UpdateWindowRequest uWindow);
        Task<AppResponse> DeleteWindow(Guid id);
    }
}
