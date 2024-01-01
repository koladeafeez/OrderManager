
namespace OrderManager.Core.HandleWindow.CreateWindow
{
    public class UpdateWindowRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        public List<UpdateElementRequest>? SubElements { get; set; } = [];

    }


    public class UpdateElementRequest
    {
        public Guid? Id { get; set; }
        public int Element { get; set; }
        public int Type { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }


}
