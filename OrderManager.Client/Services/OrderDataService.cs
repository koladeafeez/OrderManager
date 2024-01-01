using Newtonsoft.Json;
using OrderManager.Shared.Contracts.Models;
using OrderManager.Shared.Contracts.Responses;
using System.Net.Http.Json;
using System.Text.Json;
#pragma warning disable CS8603

namespace OrderManager.Client.Services
{
    public class OrderDataService(HttpClient httpClient) : IOrderDataService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<bool> AddOrder(Order newOrder)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync<Order>($"api/order",newOrder );
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> UpdateOrder(Guid orderId, Order updatedOrder)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync<Order>($"api/order/{orderId}", updatedOrder);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<Order>? GetOrderAsync(Guid id)
        {
            try
            {
                var result = await _httpClient.GetStringAsync($"api/order/{id}");

                return result == null ? null : JsonConvert.DeserializeObject<ApiResponse<Order>>(result)?.Response;
            }catch (Exception)
            {
                return new Order();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            try
            {
                var result = await _httpClient.GetStringAsync($"api/order");
                return result == null ? null : JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Order>>>(result)?.Response;
            }
            catch (Exception)
            {
                return new List<Order>();
            }
        }
        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                var result = await _httpClient.GetStringAsync($"api/order");
                return result == null ? null : JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Order>>>(result)?.Response.First();
            }
            catch (Exception)
            {
                return new Order();
            }
        }


        public async Task<Window>? GetOrderWindowAsync(Guid windowId)
        {
            try
            {
                var result = await _httpClient.GetStringAsync($"api/order/window/{windowId}");
                return result == null ? new Window() : JsonConvert.DeserializeObject<ApiResponse<Window>>(result)?.Response;
            }
            catch (Exception)
            {
                return new Window();
            }
        }


        public async Task<bool> SaveWindowAsync(Guid orderId, Window createWindowRequest)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync<Window>($"api/order/window", createWindowRequest);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateWindowAsync(Guid windowId, Window createWindowRequest)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync<Window>($"api/order/window/{windowId}", createWindowRequest);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> SaveElementAsync(Guid elementId, SubElement createWindowRequest)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync<SubElement>($"api/order/element", createWindowRequest);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateSubElementAsync(Guid elementId, SubElement createWindowRequest)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync<SubElement>($"api/order/element/{elementId}", createWindowRequest);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"api/order/{id}");

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteWindowAsync(Guid id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"api/order/window/{id}");

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteElementAsync(Guid id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"api/order/element/{id}");

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
