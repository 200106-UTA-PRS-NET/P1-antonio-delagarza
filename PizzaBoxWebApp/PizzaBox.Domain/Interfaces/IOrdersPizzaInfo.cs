using PizzaBox.Domain.Models;

using System.Collections.Generic;

namespace PizzaBox.Domain.Interfaces
{
    public interface IOrdersPizzaInfo : IRepository<OrdersPizzaInfo>
    {
        public IEnumerable<OrdersPizzaInfo> GetOrdersPizzas(int id);

    }
}
