namespace Platform.Web.Models.Orders
{
    public class OrderVm
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; }
       // public AddressDto Address { get; set; }
        public List<OrderItemVm> OrderItems { get; set; }
    }
}
