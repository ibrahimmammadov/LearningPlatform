using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order.Application.Dtos
{
    public class OrderItemDto
    {
        public string ProductID { get;  set; }
        public string ProductName { get;  set; }
        public string PicturetUrl { get;  set; }
        public decimal Price { get;  set; }
    }
}
