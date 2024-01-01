
using FluentValidation;
using OrderManager.Core.HandleElement.CreateElement;
using OrderManager.Core.HandleWindow.CreateWindow;

namespace OrderManager.Core.HandleOrder.CreateOrder
{

    public class BulkCreateOrderValidator : AbstractValidator<List<CreateOrderRequest>>
    {
        public BulkCreateOrderValidator()
        {
            RuleForEach(x => x).SetValidator(new CreateOrderValidator());
        }
    }
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {

        public CreateOrderValidator() 
        { 
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.State).NotNull().NotEmpty();
            RuleForEach(x => x.Windows).SetValidator(new CreateWindowValidator());
        }
    }

    public class CreateWindowValidator : AbstractValidator<CreateWindowRequest>
    {
        public CreateWindowValidator()
        {
            RuleFor(x => x.QuantityOfWindows).NotNull().GreaterThan(0);
            RuleFor(x => x.TotalSubElements).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleForEach(x => x.SubElements).SetValidator(new CreateSubElementValidator());
        }
    }


    public class CreateSubElementValidator : AbstractValidator<CreateElementRequest>
    {
        public CreateSubElementValidator()
        {
            RuleFor(x => x.Element).NotNull().GreaterThan(0);
            RuleFor(x => x.Width).NotNull().GreaterThan(0);
            RuleFor(x => x.Height).NotNull().GreaterThan(0);
        }
    }

}
