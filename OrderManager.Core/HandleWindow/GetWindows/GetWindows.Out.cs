using OrderManager.Core.HandleWindow.GetWindow;
using OrderManager.Shared;

namespace OrderManager.Core.HandleWindow.GetWindows
{
    public class GetWindowsOut : BaseResponseOut
    {
        public GetWindowsOut(string message, List<GetWindowResponse> response, bool success = false) : base(message, success: success)
        {
            Response = response;
        }

        public List<GetWindowResponse> Response { get; set; }
        public MetaData MetaData { get; set; }

    }
}
