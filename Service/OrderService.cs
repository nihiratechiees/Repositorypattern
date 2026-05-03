using Repopattern.Model;
using Repopattern.Repository.Interface;

namespace Repopattern.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(string OrderId, decimal TotalAmount)> CreateOrderAsync(CreateOrderRequest request)
        {
            if (request == null || !request.Items.Any())
                throw new ArgumentException("Items required");

            var orderId = Guid.NewGuid().ToString();

            var order = new Order
            {
                Id = orderId,
                CustomerName = request.CustomerId,
                OrderDate = DateTime.UtcNow
            };

            var OrderItems=new List<Orderitem>();

            decimal totalAmount = 0;

            foreach (var item in request.Items)
            {
                if (item.Quantity == null || item.UnitPrice == null)
                    throw new ArgumentException("Invalid item data");

                if (item.Quantity <= 0 || item.UnitPrice <= 0)
                    throw new ArgumentException("Quantity and UnitPrice must be greater than zero");

                var totalPrice = item.Quantity.Value * item.UnitPrice.Value;
                OrderItems.Add(new Orderitem
                {
                    OrderId = orderId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = totalPrice
                });


                totalAmount += totalPrice;
            }

            order.TotalAmount = totalAmount;

            await _unitOfWork.Orders.Save(order);
            await _unitOfWork.OrderItems.Save(OrderItems);
            await _unitOfWork.CompleteAsync();

            return (order.Id, totalAmount);
        }
    }
}
