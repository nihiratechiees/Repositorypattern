namespace Repopattern.Model
{
    public class Orderitem
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class CreateOrderRequest
    {
        public string CustomerId { get; set; }
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
