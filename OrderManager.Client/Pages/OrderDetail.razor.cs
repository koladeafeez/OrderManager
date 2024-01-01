using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using OrderManager.Client.Components;
using OrderManager.Client.Services;
using OrderManager.Shared.Contracts.Models;
using System.Reflection;

namespace OrderManager.Client.Pages
{
    public partial class OrderDetail
    {
        [Parameter]
        public string? OrderId { get; set; }

        private Order? Order { get; set; }

        [Inject] private IOrderDataService OrderDataService { get; set; } = default!;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;


        private ConfirmDialog? dialog;


        [Inject] protected ToastService? ToastService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Order = await OrderDataService.GetOrderByIdAsync(Guid.Parse(OrderId));
        }

        private async Task OnActionSelected(ChangeEventArgs e, Window window)
        {
            var value = (string)e?.Value;
            if (value == "1")
            {
                NavigationManager.NavigateTo($"/orders/{window.Id}/windows/{window.Id}");
            }
            else if (value == "2")
            {
                NavigationManager.NavigateTo($"/updateorder/{window.OrderId}");
            }
            else if (value == "3")
            {
                await HandleDeleteSelected(window.Id);

            }
        }

        private async Task HandleDeleteSelected(Guid windowId)
        {
            var confirmation = await dialog?.ShowAsync(
            title: "Are you sure you want to delete this?",
            message1: "This will delete the record. Once deleted can not be rolled back.",
            message2: "Do you want to proceed?");

            if (confirmation)
            {
                // do whatever
                var deleted = await OrderDataService.DeleteWindowAsync(windowId);

                if (deleted)
                {
                    ToastService?.Notify(new(ToastType.Success, $"Window Deleted."));
                    NavigationManager.NavigateTo($"/orders");
                }
                else
                    ToastService?.Notify(new(ToastType.Danger, $"Window Delete Fails."));
            }

          
           

        }

    }
}
