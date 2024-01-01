


using OrderManager.Shared.Contracts.Models;
using OrderManager.Shared.Contracts.Views;
using OrderManager.Shared.Enums;

namespace OrderManager.Shared.Extensions
{
#pragma warning disable CS8603 // Possible null reference return.

    public static class ModelMappingExtension
    {

        public static Window ToWindow(this CreateWindowView x)
        {
            return x == null ? new Window()
                : new Window
                {
                     Id = x.Id,
                    OrderId = x.OrderId,
                    Name = x.Name,
                    QuantityOfWindows = x.QuantityOfWindows,
                    TotalSubElements = x.TotalSubElements,
                    SubElements = x.SubElements == null ? [] : x.SubElements.Select(x => x.ToSubElement()).ToList()
                };
        }

        public static CreateSubElementView ToCreateSubElementView(this SubElement x)
        {
            return x == null ? new CreateSubElementView()
                   : new CreateSubElementView
                   {
                       Element = x.Element,
                       Height = x.Height,
                       Width = x.Width,
                       Type = x.Type,
                       WindowId = x.WindowId,
                       Id = x.Id
                        
                   };
        }

        public static SubElement ToSubElement(this CreateSubElementView x)
        {
            return x == null ? null
                   : new SubElement
                   {
                        WindowId = x.WindowId,
                       Element = x.Element,
                       Height = x.Height,
                       Width = x.Width,
                        Type = x.Type
                   };
        }


        public static CreateWindowView ToCreateWindowView(this Window x)
        {
            return x == null ? new CreateWindowView()
                : new CreateWindowView
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    Name = x.Name,
                    QuantityOfWindows = x.QuantityOfWindows,
                    TotalSubElements = x.TotalSubElements,
                    SubElements = x.SubElements == null ? [] : x.SubElements.Select(x => x.ToCreateSubElementView()).ToList()
                };
        }


        public static CreateOrderView ToCreateOrderView(this Order x)
        {
            return x == null ? null
                : new CreateOrderView
                {
                     Id = x.Id,
                    Name = x.Name,
                     State = x.State,
                      Windows = x.Windows == null ? [] : x.Windows.Select(x => x.ToCreateWindowView()).ToList()
                };
        }


        public static Order ToCreateOrder(this CreateOrderView x)
        {
            return x == null ? new Order()
                : new Order
                {
                     Id = x.Id,
                    Name = x.Name,
                    State = x.State,
                    Windows =x.Windows == null ? [] : x.Windows.Select(x => x.ToWindow()).ToList()
                };
        }




    }
#pragma warning restore CS8603 // Possible null reference return.

}
