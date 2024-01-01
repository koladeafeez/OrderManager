

using OrderManager.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Shared.Contracts.Views
{
    public class CreateSubElementView
    {
        public Guid Id { get; set; }
        public Guid WindowId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Element Must be bigger than 0")]
        public int Element { get; set; }
        public ElementType Type { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Width must be bigger than 0")]

        public double Width { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Height must be bigger than 0")]

        public double Height { get; set; }
        public int I { get; set; } // internal

        public CreateSubElementView()
        {
            Id = Guid.NewGuid();
        }

        public CreateSubElementView(int no)
        {
            Id = Guid.NewGuid();
            I = no;
        }


    }


    public class CreateSubElementViewModel
    {
        public Guid Id { get; set; }
        public Guid WindowId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Element Must be bigger than 0")]

        public int Element { get; set; }
        public ElementType Type { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Width must be bigger than 0")]

        public double Width { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Height must be bigger than 0")]

        public double Height { get; set; }
    }
}
