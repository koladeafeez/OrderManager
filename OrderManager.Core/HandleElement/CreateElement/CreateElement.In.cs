

namespace OrderManager.Core.HandleElement.CreateElement
{
    public class CreateElementRequest
    {
        public Guid WindowId { get; set; }
        public int Element { get; set; }
        public int Type { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
