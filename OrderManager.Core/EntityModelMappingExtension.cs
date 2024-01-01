
using OrderManager.Core.HandleElement.CreateElement;
using OrderManager.Core.HandleOrder.CreateOrder;
using OrderManager.Core.HandleOrder.GetOrder;
using OrderManager.Core.HandleOrder.UpdateOrder;
using OrderManager.Core.HandleWindow.CreateWindow;
using OrderManager.Core.HandleWindow.GetWindow;
using OrderManager.Data.Models;
using OrderManager.Shared.Enums;

namespace OrderManager.Core
{
    public static class EntityModelMappingExtension
    {

        public static Order ToOrder(this CreateOrderRequest x)
    => new()
    {
        Name = x.Name,
        State = x.State.ToUpper(),
        Windows = ToWindows(x.Windows),
    };


        public static GetOrderResponse ToOrderOut(this Order x)
            => new()
            {
                Id = x.Id,
                Name = x.Name,
                State = x.State.ToUpper(),
                UpdatedAt = x.UpdatedAt,
                CreatedOn = x.CreatedOn,
                Windows = ToWindowsOut(x.Windows)
            };


        public static List<GetWindowResponse>? ToWindowsOut(this IEnumerable<Window> model)
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

        public static List<GetWindowSubElementResponse>? ToSubElementOut(this IEnumerable<SubElement>? model)
           => model == null ? [] : model.Select(x => new GetWindowSubElementResponse
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



        public static List<Window> ToWindows(this IEnumerable<CreateWindowRequest>? model)
            => model == null ? [] : model.Select(x => new Window
            {
                Name = x.Name,
                QuantityOfWindows = x.QuantityOfWindows,
                TotalSubElements = x.TotalSubElements,
                SubElements = ToSubElements(x.SubElements)
            }).ToList();

        public static Window? ToWindow(this CreateWindowRequest? x)
    =>x == null ? new Window() : new Window
    {
        Name = x.Name,
         OrderId = x.OrderId,
        QuantityOfWindows = x.QuantityOfWindows,
        TotalSubElements = x.TotalSubElements,
        SubElements = ToSubElements(x.SubElements)
    };



        public static List<SubElement> ToSubElements(this IEnumerable<CreateElementRequest>? model)
            => model == null ? [] : model.Select(x => new SubElement
            {
                Element = x.Element,
                Height = x.Height,
                Width = x.Width,
                Type = ((ElementType)x.Type).ToString(),
            }).ToList();




        public static SubElement ToSubElement(this CreateElementRequest x)
        => x == null ? new SubElement() : new SubElement
        {
            WindowId = x.WindowId,
            Element = x.Element,
            Height = x.Height,
            Width = x.Width,
            Type = ((ElementType)x.Type).ToString(),
        };



        public static SubElement ToUpdatedSubElement(this SubElement x, UpdateElementRequest y)
        {
            if (x == null)
                return new SubElement();

            x.Element = y.Element <= 0 ? x.Element : y.Element;
            x.Height = y.Height <= 0 ? x.Height :  y.Height;
            x.Width =y.Width <= 0 ? x.Width :  y.Width;
            x.Type = string.IsNullOrEmpty(((ElementType)y.Type).ToString()) ? x.Type : ((ElementType)y.Type).ToString();

            return x;
        }

        public static Order ToUpdatedOrder(this Order x, UpdateOrderRequest y)
        {
            if (x == null)
                return new Order();

            x.Name = string.IsNullOrEmpty(y.Name) ? x.Name : y.Name;
            x.State = string.IsNullOrEmpty(y.State) ? x.State : y.State;
            var orderWindow = new List<Window>();

            foreach (var itm in y.Windows)
            {
                if (itm.Id != null && itm.Id != Guid.Empty)
                {
                    var updatedWindow = x.Windows.FirstOrDefault()?.ToUpdatedWindow(itm);
                    if (updatedWindow != null)
                        orderWindow.Add(updatedWindow);
                }
                else 
                {
                    orderWindow.Add(new Window() { OrderId = x.Id }.ToUpdatedWindow(itm));
                }
            }
            if (orderWindow.Count > 0)
                x.Windows = orderWindow;
            return x;
        }

        public static Window ToUpdatedWindow(this Window x, UpdateWindowRequest y)
        {
            x.Name = string.IsNullOrEmpty(y.Name) ? x.Name : y.Name;
            x.QuantityOfWindows =y.QuantityOfWindows <= 0 ? x.QuantityOfWindows : y.QuantityOfWindows;
            x.TotalSubElements = y.TotalSubElements <= 0 ? x.TotalSubElements : y.TotalSubElements;
            var orderWindow = new List<SubElement>();
            foreach (var itm in y.SubElements)
            {
                if (itm.Id != null && itm.Id != Guid.Empty)
                {
                    var updatedWindow = x.SubElements.FirstOrDefault()?.ToUpdatedSubElement(itm);
                    if (updatedWindow != null)
                        orderWindow.Add(updatedWindow);
                }
                else
                {
                    orderWindow.Add(new SubElement() { WindowId = x.Id }.ToUpdatedSubElement(itm));
                }
            }
            if (orderWindow.Count > 0)
                x.SubElements = orderWindow;

            return x;
        }

    }
}
