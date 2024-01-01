

using OrderManager.Core.HandleElement.CreateElement;
using OrderManager.Core.HandleWindow.CreateWindow;
using OrderManager.Shared;

namespace OrderManager.Core.Services
{
    public interface IElementService
    {
        AppResponse CreateElement(CreateElementRequest model);
        Task<AppResponse> DeleteElement(Guid elementId);
        Task<AppResponse> UpdateElement(Guid id,UpdateElementRequest uElement);
    }
}
