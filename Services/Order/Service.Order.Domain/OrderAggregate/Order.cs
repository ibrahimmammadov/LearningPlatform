using Service.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get;private set; }
        public string BuyerId { get;private set; }
        public Address Address { get;private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {

        }

        public Order(string buyerid, Address address)
        {
            BuyerId = buyerid;
            Address = address;
            CreatedDate = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(string productid,string productname, decimal price,string picturesurl)
        {
            var existproduct = _orderItems.Any(x => x.ProductID == productid);
            if (!existproduct)
            {
                _orderItems.Add(new OrderItem(productid, productname, picturesurl, price));
            }
        }

        public decimal GetTotalPrice =>_orderItems.Sum(x => x.Price);
    }
}
