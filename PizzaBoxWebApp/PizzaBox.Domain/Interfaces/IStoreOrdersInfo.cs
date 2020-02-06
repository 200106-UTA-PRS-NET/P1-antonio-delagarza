using PizzaBox.Domain.Models;

using System.Collections.Generic;


namespace PizzaBox.Domain.Interfaces
{
    public interface IStoreOrdersInfo : IRepository<StoreOrdersInfo>
    {
        public int GetStoreId(int orderId);
        public IEnumerable<StoreOrdersInfo> GetStoreOrders(int id);
    }
}
