using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using OrderManager.Client.Services;
using OrderManager.Shared.Contracts.Views;
using OrderManager.Shared.Extensions;

namespace OrderManager.Client.Pages
{
    public partial class OrderForm
    {
        [Parameter]
        public string? OrderId { get; set; }
        public CreateOrderView CreateOrderView = new();

        [Inject]
        private IOrderDataService OrderDataService { get; set; } = default!;

        [Inject] protected ToastService? ToastService { get; set; }

        private async Task SubmitOrder()
        {

            var iscreated = await OrderDataService.AddOrder(CreateOrderView.ToCreateOrder());

            if (iscreated)
            {
                ToastService?.Notify(new(ToastType.Success, $"Order Created."));
                NavigationManager.NavigateTo("/orders");

            }
            else
            {
                ToastService?.Notify(new(ToastType.Danger, $"Order Creation Fails."));
            }
        }



        public void AddWindow()
        {
            CreateOrderView.Windows.Add(new CreateWindowView());
        }

        public void DeleteWindow(Guid windowId)
        {
            var window = CreateOrderView.Windows.FirstOrDefault(x => x.Id == windowId);
            if (window != null)
            {
                CreateOrderView.Windows.Remove(window);
            }
        }

        public void AddSubElement(Guid windowId)
        {
            var subElements = CreateOrderView.Windows.First(x =>x.Id == windowId).SubElements;
            var i = 1;
            if (subElements.Count > 0)
            {
                i = subElements.Max(s => s.I) + 1;
            }
            subElements.Add(new CreateSubElementView(i));
        }

        public void DeleteSubElement(Guid windowId, Guid subElementId)
        {
            var window = CreateOrderView.Windows.FirstOrDefault(x => x.Id == windowId);
            var element = window?.SubElements.FirstOrDefault(x => x.Id == subElementId);
            if (element != null)
            {
                window?.SubElements.Remove(element);
            }
        }



    }
}
