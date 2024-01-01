
using OrderManager.Data.Models;
using OrderManager.Shared;

namespace OrderManager.Core.HandleWindow.GetWindow
{

    public class GetWindowOut : BaseResponseOut
    {
        public GetWindowOut(string message, GetWindowResponse response, bool success = false) : base(message, success: success)
        {
            Response = response;
        }

        public GetWindowResponse Response { get; set; }

    }

    public class GetWindowResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public ICollection<GetWindowSubElementResponse>? SubElements { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class GetWindowSubElementResponse
    {
        public Guid Id { get; set; }
        public Guid WindowId { get; set; }
        public int Element { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
