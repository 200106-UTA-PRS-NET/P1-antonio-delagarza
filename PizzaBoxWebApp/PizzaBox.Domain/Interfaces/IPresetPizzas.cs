using PizzaBox.Domain.Models;


namespace PizzaBox.Domain.Interfaces
{
    public interface IPresetPizzas : IRepository<PresetPizzas>
    {
        public PresetPizzas GetPizza(string name);
    }
}
