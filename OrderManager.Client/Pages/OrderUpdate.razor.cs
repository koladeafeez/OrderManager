using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using OrderManager.Client.Services;
using OrderManager.Shared.Contracts.Views;
using OrderManager.Shared.Extensions;

namespace OrderManager.Client.Pages
{
    public partial class OrderUpdate
    {
        [Parameter]
        public string? OrderId { get; set; }

        public CreateOrderView CreateOrderView = new();

        [Inject]
        private IOrderDataService OrderDataService { get; set; } = default!;

        [Inject] protected ToastService? ToastService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
                var orderResponse = await OrderDataService.GetOrderByIdAsync(Guid.Parse(OrderId));
                CreateOrderView = orderResponse != null ? orderResponse.ToCreateOrderView() : new CreateOrderView();
        }

        private async Task UpdateOrder()
        {
           var isupdated = await OrderDataService.UpdateOrder(Guid.Parse(OrderId), CreateOrderView.ToCreateOrder());

            if (isupdated)
            {
                ToastService?.Notify(new(ToastType.Success, $"Order Updated."));
                NavigationManager.NavigateTo("/orders");

            }
            else
                ToastService?.Notify(new(ToastType.Danger, $"Order Update Fails."));

        }


        public void AddWindow()
        {
            CreateOrderView.Windows.Add(new CreateWindowView());
        }


        public void AddSubElement(Guid windowId)
        {
            var subElements = CreateOrderView.Windows.First(w => w.Id == windowId).SubElements;
            var nextElementNo = 1;
            if (subElements.Count > 0)
            {
                nextElementNo = subElements.Max(s => s.I) + 1;
            }
            subElements.Add(new CreateSubElementView(nextElementNo));
        }

        public void DeleteSubElement(Guid windowId, Guid subElementId)
        {
            var window = CreateOrderView.Windows.FirstOrDefault(w => w.Id == windowId);
            var element = window?.SubElements.FirstOrDefault(e => e.Id == subElementId);
            if (element != null)
            {
                window?.SubElements.Remove(element);
            }
        }
    }
}
