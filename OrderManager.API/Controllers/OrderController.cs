using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Core.HandleElement.CreateElement;
using OrderManager.Core.HandleOrder.CreateOrder;
using OrderManager.Core.HandleOrder.UpdateOrder;
using OrderManager.Core.HandleWindow.CreateWindow;
using OrderManager.Core.Services;
using OrderManager.Shared;

namespace OrderManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService, IWindowService windowService, IElementService elementService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IWindowService _windowService = windowService;
        private readonly IElementService _elementService = elementService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var result = await _orderService.GetOrdersAsync();
            return StatusCode((int)result._httpStatusCode, result.Response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await _orderService.GetOrderAsync(Id);
            return StatusCode((int)result._httpStatusCode, result.Response);
        }

        [HttpGet("{Id:guid}/window")]
        public IActionResult GetOrderWindows([FromRoute] Guid Id)
        {
            var result =  _windowService.GetWindowsByOrderAsync(Id);
            return StatusCode((int)result._httpStatusCode, result.Response);
        }

        #region Order CRUD

        [HttpPost]
        public IActionResult Create([FromBody] CreateOrderRequest model)
        {
            var validator = new CreateOrderValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponseOut("Request Body Is Invalid", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var result =_orderService.CreateOrder(model);

            return StatusCode((int)result._httpStatusCode, result.Response);
        }

        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid orderId, [FromBody] UpdateOrderRequest model)
        {
            var validator = new UpdateOrderValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponseOut("Request Body Is Invalid", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

           var result = await _orderService.UpdateOrder(orderId,model);
            return StatusCode((int)result._httpStatusCode, result.Response);
        }




        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid orderId)
        {
            var result = await _orderService.DeleteOrder(orderId);

            return StatusCode((int)result._httpStatusCode, result.Response);
        }


        #endregion

        #region Window CRUD

        [HttpPost("window")]
        public async Task<IActionResult> CreateWindow([FromBody] CreateWindowRequest model)
        {
            var validator = new CreateWindowValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponseOut("Request Body Is Invalid", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            var result = await _orderService.CreateWindow(model);

            return StatusCode((int)result._httpStatusCode, result.Response);

        }

        [HttpGet("window/{windowId}")]
        public IActionResult GetWindow([FromRoute] Guid windowId)
        {
            var result = _windowService.GetWindowById(windowId);

            return StatusCode((int)result._httpStatusCode, result.Response);

        }

        [HttpPut("window/{windowId}")]
        public async Task<IActionResult> UpdateWindow([FromRoute] Guid windowId, [FromBody] UpdateWindowRequest model)
        {
            var validator = new UpdateWindowValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponseOut("Request Body Is Invalid", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            var result =await _orderService.UpdateWindow(windowId,model);

            return StatusCode((int)result._httpStatusCode, result.Response);

        }


        [HttpDelete("window/{windowId}")]
        public async Task<IActionResult> DeleteWindow([FromRoute] Guid windowId)
        {
            var result = await _orderService.DeleteWindow(windowId);

            return StatusCode((int)result._httpStatusCode, result.Response);

        }


        #endregion


        [HttpPost("element")]
        public IActionResult CreateElement([FromBody] CreateElementRequest model)
        {
            var validator = new CreateSubElementValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponseOut("Request Body Is Invalid", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            var result = _elementService.CreateElement(model);

            return StatusCode((int)result._httpStatusCode, result.Response);

        }

        [HttpPut("element/{elementId}")]
        public async Task<IActionResult> DeleteElement([FromRoute] Guid elementId, [FromBody] UpdateElementRequest model)
        {
            var validator = new UpdateElementValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BaseResponseOut("Request Body Is Invalid", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            var result = await _elementService.UpdateElement(elementId,model);

            return StatusCode((int)result._httpStatusCode, result.Response);
        }



        [HttpDelete("element/{elementId}")]
        public async Task<IActionResult> DeleteElement([FromRoute] Guid elementId)
        {
            var result = await _elementService.DeleteElement(elementId);

            return StatusCode((int)result._httpStatusCode, result.Response);

        }


    }
}
