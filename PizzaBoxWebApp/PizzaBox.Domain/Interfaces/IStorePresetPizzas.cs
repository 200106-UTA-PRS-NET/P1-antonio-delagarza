using PizzaBox.Domain.Models;
using System.Collections.Generic;


namespace PizzaBox.Domain.Interfaces
{
    public interface IStorePresetPizzas : IRepository<StorePresetPizzas>
    {
        public IEnumerable<StorePresetPizzas> GetStorePizzas(int id);
    }
}
