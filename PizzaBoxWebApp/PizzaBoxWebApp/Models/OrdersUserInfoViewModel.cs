using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWebApp.Models
{
    public class OrdersUserInfoViewModel
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
