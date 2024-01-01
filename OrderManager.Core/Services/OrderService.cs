
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManager.Core.HandleOrder.CreateOrder;
using OrderManager.Core.HandleOrder.GetOrder;
using OrderManager.Core.HandleOrder.GetOrders;
using OrderManager.Core.HandleOrder.UpdateOrder;
using OrderManager.Core.HandleWindow.CreateWindow;
using OrderManager.Data;
using OrderManager.Data.Models;
using OrderManager.Data.Models.Repositories;
using OrderManager.Shared;
using System.Net;


namespace OrderManager.Core.Services
{
    public class OrderService(IBaseRepository<Order> baseRepo, IResponseFactory response, AppDbContext dbContext, ILogger<OrderService> logger) 
        : BaseService<Order, CreateOrderRequest>(baseRepo, response), IOrderService
    {
        private AppDbContext Context { get; set; } = dbContext;
        private readonly ILogger<OrderService> _logger = logger;

        public async Task<AppResponse> GetOrdersAsync()
        {
            var orders = await Context.Order.Include(x => x.Windows).ThenInclude(x => x.SubElements).ToListAsync();

            if (orders.Count == 0)
            {
                return _response.Success(new GetOrdersOut("Order Is Currently Empty",
                    [], true));
            }

            var data = orders.Select(x => x.ToOrderOut()).ToList();
            return _response.Success(new GetOrdersOut("Displaying Orders",
                data, true));

        }


        public async Task<AppResponse> GetOrderAsync(Guid orderId)
        {
            var order = await Context.Order.Include(x => x.Windows).ThenInclude(x => x.SubElements).FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                return _response.Error("Order Not Found", HttpStatusCode.BadRequest);
            }

            var data = order.ToOrderOut();
            return _response.Success(new GetOrderOut("Displaying Order",
                data, true));

        }



        public async Task<AppResponse> UpdateOrder(Guid id, UpdateOrderRequest updatedOrder)
        {
            try
            {
                updatedOrder.Id = id;
                var order = await Context.Order.Include(x => x.Windows).ThenInclude(x => x.SubElements).FirstOrDefaultAsync(x => x.Id == id);

                if (order == null)
                    return _response.Error("Order Not Found", HttpStatusCode.BadRequest);


                Context.Order.Update(order.ToUpdatedOrder(updatedOrder));
                await Context.SaveChangesAsync();
                return _response.Success(new BaseResponseOut("Order Updated"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Order Fails");
                return _response.Error("Fails to Update Order", HttpStatusCode.InternalServerError);
            }
        }


        public async Task<AppResponse> DeleteOrder(Guid orderId)
        {
            try
            {
                var order = await GetByIdAsync(orderId);
                if (order == null)
                    return _response.Error("Order Not Found", HttpStatusCode.BadRequest);

                Context.Order.Remove(order);
                await Context.SaveChangesAsync();

                return _response.Success(new BaseResponseOut("Order Deleted"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Order Fails");
                return _response.Error("Fails to Delete Order", HttpStatusCode.InternalServerError);
            }

        }

        public AppResponse CreateOrder(CreateOrderRequest order)
        {
            try
            {
                baseRepository.Insert(order.ToOrder());
                baseRepository.Save();
                return _response.Success(new BaseResponseOut("Order Created"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create Order Failure");
                return _response.Error("Create Order Fail", HttpStatusCode.InternalServerError);
            }
        }




        public async Task<AppResponse> DeleteWindow(Guid id)
        {
            try
            {
                var window = await Context.Window.Include(x => x.SubElements)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (window == null)
                   return _response.Error("Window Not Found", HttpStatusCode.BadRequest);

                Context.Window.Remove(window);
                await Context.SaveChangesAsync();
                return _response.Success(new BaseResponseOut("Window Deleted"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Order Failure");
                return _response.Error("Delete Order Fail", HttpStatusCode.InternalServerError);
            }
        }


        public async Task<AppResponse> CreateWindow(CreateWindowRequest newWindow)
        {
            try
            {
                var order = await GetByIdAsync(newWindow.OrderId);

                if (order == null)
                    return _response.Error("Invalid OrderId", HttpStatusCode.BadRequest);

                var window = newWindow.ToWindow();
                Context.Window.Add(window);
                await Context.SaveChangesAsync();
                return _response.Success(new BaseResponseOut("Window Created"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create Window Failure");
                return _response.Error("Create Window Fail", HttpStatusCode.InternalServerError);
            }
        }



        public async Task<AppResponse> UpdateWindow(Guid id, UpdateWindowRequest uWindow)
        {
            try
            {
                var window = await Context.Window.Include(x => x.SubElements).FirstOrDefaultAsync(x => x.Id == id);

                if (window == null)
                {
                    return _response.Error("Window Not Found", HttpStatusCode.BadRequest);
                }
                    uWindow.Id = id;
                    var updatedWindow = window.ToUpdatedWindow(uWindow);

                Context.Window.Update(updatedWindow);
                await Context.SaveChangesAsync();
                return _response.Success(new BaseResponseOut("Window Updated"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Window Failure");
                return _response.Error("Update Window Fail", HttpStatusCode.InternalServerError);
            }
        }



    }

}
