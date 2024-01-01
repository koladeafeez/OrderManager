using Microsoft.AspNetCore.Components;
using OrderManager.Client.Services;
using OrderManager.Shared.Contracts.Models;

namespace OrderManager.Client.Pages
{
    public partial class Orders
    {
        private IEnumerable<Order>? orders;

        [Inject] private IOrderDataService OrderDataService { get; set; } = default!;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            orders = await OrderDataService.GetOrdersAsync();
        }

        private async Task OnActionSelected(ChangeEventArgs e, Order order)
        {
            var value = (string)e?.Value;
            if (value == "1")
            {
                NavigationManager.NavigateTo($"/orders/{order.Id}");
            }
            else if (value == "2")
            {
                NavigationManager.NavigateTo($"/updateorder/{order.Id}");
            }
            else if (value == "3")
            {
                await HandleDeleteSelected(order.Id);

            }
        }

        private async Task HandleDeleteSelected(Guid orderId)
        {
            await OrderDataService.DeleteOrderAsync(orderId);
        }
    }
}
