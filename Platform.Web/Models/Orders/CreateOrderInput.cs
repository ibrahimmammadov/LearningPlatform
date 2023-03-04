namespace Platform.Web.Models.Orders
{
    public class CreateOrderInput
    {
        public CreateOrderInput()
        {
            OrderItems = new List<CreateOrdemItemInput>();
        }
        public string BuyerId { get; set; }
        public List<CreateOrdemItemInput> OrderItems { get; set; }
        public CreateAddressInput Address { get; set; }
    }
}
