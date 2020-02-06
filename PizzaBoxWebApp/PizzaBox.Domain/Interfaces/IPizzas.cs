using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Interfaces
{
    public interface IPizzas : IRepository<Pizzas>
    {
        public Pizzas GetPizzasbyId(int id);
    }
}
