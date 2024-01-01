using Microsoft.Extensions.Logging;
using OrderManager.Data.Models.Repositories;
using OrderManager.Data.Models;
using OrderManager.Data;
using OrderManager.Core.HandleElement.CreateElement;
using OrderManager.Shared;
using System.Net;
using Microsoft.EntityFrameworkCore;
using OrderManager.Core.HandleWindow.CreateWindow;


namespace OrderManager.Core.Services
{
    public class ElementService(IBaseRepository<SubElement> baseRepo, IResponseFactory response, AppDbContext dbContext, ILogger<OrderService> logger) 
        : BaseService<SubElement, CreateElementRequest>(baseRepo, response), IElementService
    {
        private AppDbContext Context { get; set; } = dbContext;
        private readonly ILogger<OrderService> _logger = logger;

        public AppResponse CreateElement(CreateElementRequest model)
        {
            try
            {
                var window = Context.SubElement.FirstOrDefault(x => x.WindowId == model.WindowId);
                if (window == null)
                    return _response.Error("Window Not Found", HttpStatusCode.BadRequest);

                baseRepository.Insert(model.ToSubElement());
                baseRepository.Save();
                return _response.Success(new BaseResponseOut("ELement Created"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Crate Elememnt Failure");
                return _response.Error("Create Element Fail", HttpStatusCode.InternalServerError);
            }
        }



        public async Task<AppResponse> UpdateElement(Guid id, UpdateElementRequest uElement)
        {
            try
            {

                var element = await GetByIdAsync(id);

                if (element == null)
                {
                    return _response.Error("Element Not Found", HttpStatusCode.BadRequest);
                }
                    uElement.Id = id;
                    var updatedWindow = element.ToUpdatedSubElement(uElement);

                Context.SubElement.Update(updatedWindow);
                await Context.SaveChangesAsync();
                return _response.Success(new BaseResponseOut("Element Updated"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Element Failure");
                return _response.Error("Update Element Fail", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<AppResponse> DeleteElement(Guid elementId)
        {
            try
            {
                var element = await GetByIdAsync(elementId);

                if (element != null)
                {

                    Context.SubElement.Remove(element);
                    await Context.SaveChangesAsync();
                }
                return _response.Success(new BaseResponseOut("Element Updated"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Element Failure");
                return _response.Error("Delete Element Fail", HttpStatusCode.InternalServerError);
            }
        }

    }
}
