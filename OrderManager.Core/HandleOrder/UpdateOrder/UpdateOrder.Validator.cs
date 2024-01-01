using FluentValidation;
using OrderManager.Core.HandleWindow.CreateWindow;


namespace OrderManager.Core.HandleOrder.UpdateOrder
{

    public class UpdateOrderValidator : AbstractValidator<UpdateOrderRequest>
    {

        public UpdateOrderValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.State).NotNull().NotEmpty();
            RuleForEach(x => x.Windows).SetValidator(new UpdateWindowValidator());
        }
    }


    public class UpdateWindowValidator : AbstractValidator<UpdateWindowRequest>
    {

        public UpdateWindowValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.QuantityOfWindows).NotNull().Must(x => ( x > 0)).WithMessage("Quantity of Windows Must Be greaterthan 0");
            RuleFor(x => x.TotalSubElements).NotNull().Must(x => ( x > 0)).WithMessage("TotalSubElements Must Be greaterthan 0");
            RuleForEach(x => x.SubElements).SetValidator(new UpdateElementValidator());
        }
    }


    public class UpdateElementValidator : AbstractValidator<UpdateElementRequest>
    {

        public UpdateElementValidator()
        {
            RuleFor(x => x.Element).NotNull().Must(x => (x > 0)).WithMessage("Element Must Be greaterthan 0");
            RuleFor(x => x.Width).NotNull().Must(x => (x > 0)).WithMessage("Width Must Be greaterthan 0");
            RuleFor(x => x.Height).NotNull().Must(x => ( x > 0)).WithMessage("Height Must Be greaterthan 0");
        }
    }

}
