using OrderManager.Shared.Contracts.Models;

namespace OrderManager.Client.Services
{
    public interface IOrderDataService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order>? GetOrderAsync(Guid id);
        Task<bool> AddOrder(Order newOrder);
        Task<Window>? GetOrderWindowAsync(Guid windowId);
        Task<bool> SaveWindowAsync(Guid orderId, Window createWindowRequest);
        Task<bool> UpdateOrder(Guid orderId, Order updatedOrder);
        Task<bool> UpdateWindowAsync(Guid windowId, Window createWindowRequest);
        Task<bool> SaveElementAsync(Guid elementId, SubElement createWindowRequest);
        Task<bool> UpdateSubElementAsync(Guid elementId, SubElement createWindowRequest);
        Task<bool> DeleteOrderAsync(Guid id);
        Task<bool> DeleteWindowAsync(Guid id);
        Task<bool> DeleteElementAsync(Guid id);
        Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}
