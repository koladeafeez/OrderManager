

using System.ComponentModel.DataAnnotations;

namespace OrderManager.Shared.Contracts.Views
{
    public class CreateWindowView
    {
        public CreateWindowView()
        {
            Id = Guid.NewGuid();
            SubElements = [];
        }

        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum name character is two")]
        public string Name { get; set; }
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Width should be more than 0")]
        public int QuantityOfWindows { get; set; }
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Width should be more than 0")]
        public int TotalSubElements { get; set; }
        [ValidateComplexType, ValidateItemCount(1)]
        public List<CreateSubElementView> SubElements { get; set; }

    }

    public class CreateWindowViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum name character is two")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Width should be more than 0")]
        public int QuantityOfWindows { get; set; }
        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Width should be more than 0")]
        public int TotalSubElements { get; set; }
        public CreateSubElementViewModel SubElements { get; set; } = new CreateSubElementViewModel();

        public string Status { get; set; } = "Not Set";
    }
}
