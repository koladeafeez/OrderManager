
using System.ComponentModel.DataAnnotations;
using System.Collections;


namespace OrderManager.Shared.Contracts.Views
{

    public class CreateOrderView
    {

        public CreateOrderView()
        {
            Id = Guid.NewGuid();
            Windows = [];
        }
        public Guid Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(2), MaxLength(2)]
        public string State { get; set; } = string.Empty;
        [ValidateComplexType, ValidateItemCount(1)]
        public List<CreateWindowView> Windows { get; set; }

    }


    public class ValidateItemCount(int count) : ValidationAttribute
    {
        private readonly int _count = count;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;

            var result = list?.Count >= _count;

#pragma warning disable CS8603 // Possible null reference return.
            return result
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.DisplayName} must contain minimum {_count} item");
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

}
