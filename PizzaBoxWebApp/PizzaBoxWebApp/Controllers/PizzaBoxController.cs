using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Domain.Models;

namespace PizzaBoxWebApp.Controllers
{
    public class PizzaBoxController
    {
        private readonly PizzaDBContext _context;

        public PizzaBoxController(PizzaDBContext context)
        {
            _context = context;
        }
    }
}
