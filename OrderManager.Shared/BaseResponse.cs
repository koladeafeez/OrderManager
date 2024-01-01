using System.Net;

namespace OrderManager.Shared
{
    public class AppResponse
    {
        private BaseResponseOut? _response;
        public HttpStatusCode _httpStatusCode;

        public AppResponse(BaseResponseOut response, HttpStatusCode statusCode)
        {
            _response = response;
            _httpStatusCode = statusCode;
        }

        public BaseResponseOut? Response { get => _response; }
    }


    public class BaseResponseOut
    {
        public BaseResponseOut(string message, List<string>? errors = null, bool success = false)
        {
            Message = message;
            Errors = errors ?? new();
            Success = success;
        }

        public string Message { get; set; } = string.Empty;

        public List<string> Errors { get; set; } = new();

        public bool Success { get; set; } = false;
    }
}