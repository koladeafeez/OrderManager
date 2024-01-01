using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using OrderManager.Client.Services;
using OrderManager.Shared.Contracts.Models;

namespace OrderManager.Client.Pages
{
    public partial class WindowDetail
    {
        [Parameter]
        public string? OrderId { get; set; }
        [Parameter]
        public string? WindowId { get; set; }

        private Window? Window { get; set; }

        [Inject] private IOrderDataService OrderDataService { get; set; } = default!;

        [Inject] protected ToastService? ToastService { get; set; }

        private ConfirmDialog? dialog;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            Window = await OrderDataService.GetOrderWindowAsync(Guid.Parse(WindowId));
        }

        private async Task OnActionSelected(ChangeEventArgs e, SubElement element)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var value = (string)e?.Value;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (value == "1")
            {
                await HandleDeleteSelected(element.Id);

            }
        }

        private async Task HandleDeleteSelected(Guid elementId)
        {
            var confirmation = await dialog?.ShowAsync(
           title: "Are you sure you want to delete this?",
           message1: "This will delete the record. Once deleted can not be rolled back.",
           message2: "Do you want to proceed?");

            if (confirmation)
            {
                var deleted = await OrderDataService.DeleteElementAsync(elementId);

                if (deleted)
                {
                    ToastService?.Notify(new(ToastType.Success, $"Element Deleted."));
                    NavigationManager.NavigateTo($"/orders");
                }
                else
                    ToastService?.Notify(new(ToastType.Danger, $"Element Delete Fails."));
            }


        }
    }
}
