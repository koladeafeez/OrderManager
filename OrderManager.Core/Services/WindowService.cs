

using Microsoft.Extensions.Logging;
using OrderManager.Core.HandleElement.CreateElement;
using OrderManager.Core.HandleOrder.CreateOrder;
using OrderManager.Core.HandleWindow.CreateWindow;
using OrderManager.Core.HandleWindow.GetWindow;
using OrderManager.Core.HandleWindow.GetWindows;
using OrderManager.Data.Models;
using OrderManager.Data.Models.Repositories;
using OrderManager.Shared;
using OrderManager.Shared.Enums;
using System.Net;

namespace OrderManager.Core.Services
{
    public class WindowService(IBaseRepository<Window> baseRepo, IResponseFactory response, ILogger<WindowService> logger) 
        : BaseService<Window, CreateOrderRequest>(baseRepo, response), IWindowService
    {
        private readonly ILogger<WindowService> _logger = logger;

        public AppResponse GetWindowById(Guid windowId)
        {
            try
            {
                var window = baseRepository.GetSingle(x => x.Id == windowId, x => x.SubElements);

                if (window == null)
                    return _response.Error("Window Not Found", HttpStatusCode.BadRequest);

                return _response.Success(new GetWindowOut("Displaying Window", ToWindowOut(window)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Window Failed");
                return _response.Error("Something Went Wrong", HttpStatusCode.InternalServerError);

            }

        }



        public AppResponse GetWindowsByOrderAsync(Guid orderId)
        {
            var windows = GetMultiple(x => x.OrderId == orderId);

            if (windows == null || !windows.Any())
            {
                return _response.Error("Windows Not Found", HttpStatusCode.BadRequest);
            }

            var data = ToWindowsOut(windows?.ToList());
            return _response.Success(new GetWindowsOut("Displaying Order Windows",
                data, true));

        }


        public AppResponse GetWindowSubElementsByWindowIdAsync(Guid Id)
        {
            var window = GetSingle(x => x.Id == Id, x => x.SubElements);

            if (window == null)
            {
                return _response.Error("Window Not Found", HttpStatusCode.BadRequest);
            }

            var data = ToWindowOut(window);
            return _response.Success(new GetWindowOut("Displaying Window",
                data, true));

        }


        public static List<GetWindowResponse>? ToWindowsOut(IEnumerable<Window> model)
           => model?.Select(x => new GetWindowResponse
           {
               Id = x.Id,
               Name = x.Name,
               OrderId = x.OrderId,
               TotalSubElements = x.TotalSubElements,
               QuantityOfWindows = x.QuantityOfWindows,
               UpdatedAt = x.UpdatedAt,
               CreatedOn = x.CreatedOn,
               SubElements = ToSubElementOut(x.SubElements)
           }).ToList();


        public static GetWindowResponse? ToWindowOut(Window x)
       => x == null ? null : new GetWindowResponse
       {
           Id = x.Id,
           Name = x.Name,
           OrderId = x.OrderId,
           TotalSubElements = x.TotalSubElements,
           QuantityOfWindows = x.QuantityOfWindows,
           UpdatedAt = x.UpdatedAt,
           CreatedOn = x.CreatedOn,
           SubElements = x.SubElements == null ? [] : ToSubElementOut(x.SubElements)
       };


        public static List<GetWindowSubElementResponse>? ToSubElementOut(IEnumerable<SubElement> model)
           => model?.Select(x => new GetWindowSubElementResponse
           {
               Id = x.Id,
               Element = x.Element,
               Height = x.Height,
               Width = x.Width,
               Type = x.Type,
               WindowId = x.WindowId,
               UpdatedAt = x.UpdatedAt,
               CreatedOn = x.CreatedOn,
           }).ToList();


    }


}

