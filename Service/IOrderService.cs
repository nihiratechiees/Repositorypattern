using Repopattern.Model;

namespace Repopattern.Service
{
    public interface IOrderService
    {
        Task<(string OrderId, decimal TotalAmount)> CreateOrderAsync(CreateOrderRequest request);
    }
}

