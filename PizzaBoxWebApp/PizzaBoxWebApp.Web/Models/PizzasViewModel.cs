using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWebApp.Models
{
    public class PizzasViewModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }
        public string CrustFlavor { get; set; }
        public string Sauce { get; set; }
        public string SauceAmount { get; set; }
        public string CheeseAmount { get; set; }
        public string Topping1 { get; set; }
        public string Topping2 { get; set; }
        public string Topping3 { get; set; }
        public decimal Price { get; set; }
    }
}
