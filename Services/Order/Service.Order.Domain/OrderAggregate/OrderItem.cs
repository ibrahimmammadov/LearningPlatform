using Service.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        public string ProductID { get;private set; }
        public string ProductName { get; private set; }
        public string PicturetUrl { get; private set; }
        public decimal Price { get; private set; }
        public OrderItem()
        {

        }
        public OrderItem(string productID, string productName, string picturetUrl, decimal price)
        {
            ProductID = productID;
            ProductName = productName;
            PicturetUrl = picturetUrl;
            Price = price;
        }

        public void UpdateOrderItem (string productName, string picturetUrl, decimal price)
        {
            ProductName = productName;
            PicturetUrl = picturetUrl;
            Price = price;
        }
    }
}
