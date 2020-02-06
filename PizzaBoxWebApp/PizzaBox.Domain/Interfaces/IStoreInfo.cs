using PizzaBox.Domain.Models;


namespace PizzaBox.Domain.Interfaces
{
    public interface IStoreInfo : IRepository<StoreInfo>
    {
        public void SetStore(int id, ref StoreInfo st);
        public bool FindStore(int id);
        public string GetStoreName(int id);
    }
}
