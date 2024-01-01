using OrderManager.Shared;
using System.Net;

namespace OrderManager.Core.Services
{

    public class ResponseFactory : IResponseFactory
    {
        public AppResponse Success(BaseResponseOut response, HttpStatusCode httpStatusCode = HttpStatusCode.OK) =>
            new(response, httpStatusCode);

        public AppResponse Error(string message, HttpStatusCode statusCodeError, List<string>? errors = null) =>
            new(new BaseResponseOut(message,errors), statusCodeError);

    }


    public interface IResponseFactory
    {
        AppResponse Success(BaseResponseOut response, HttpStatusCode httpStatusCode = HttpStatusCode.OK);
        AppResponse Error(string message, HttpStatusCode statusCodeError, List<string>? errors = null);
    }
}
