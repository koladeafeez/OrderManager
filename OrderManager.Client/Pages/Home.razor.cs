using Microsoft.AspNetCore.Components;
using OrderManager.Client.Services;

namespace OrderManager.Client.Pages
{
    public partial class Home
    {
        private int OrderCount { get; set; }

        [Inject] private IOrderDataService OrderDataService { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
           var orders = await OrderDataService.GetOrdersAsync();
            OrderCount = orders.Count();
        }


    }
}
