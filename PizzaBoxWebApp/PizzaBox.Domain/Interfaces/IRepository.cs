
using System.Collections.Generic;

namespace PizzaBox.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetItems();
        void Add(T item);
        void Modify(T item);
        int GetNumItems();

    }
}
