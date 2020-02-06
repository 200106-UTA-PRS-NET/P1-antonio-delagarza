using System;
using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using System.Linq;

namespace PizzaBox.Storing.Repositories
{
    public class RepositoryStoreInfo : IStoreInfo
    {
        PizzaDBContext db;

        public RepositoryStoreInfo()
        {
            db = new PizzaDBContext();
        }
        public RepositoryStoreInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StoreInfo item)
        {
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId))
            {
                Console.WriteLine("Store with this store id exists");
            }
            else
            {
                db.StoreInfo.Add(item);
                db.SaveChanges();
                Console.WriteLine("Store craeted successfully");
                
            }
            
        }

        public IEnumerable<StoreInfo> GetItems()
        {
            var query = from e in db.StoreInfo
                        select e;

            return query;
        }

        public void Modify(StoreInfo item)
        {
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId))
            {
                StoreInfo updateStore = db.StoreInfo.FirstOrDefault(e => e.StoreId == item.StoreId);
                updateStore.StoreName = item.StoreName;
                updateStore.Address = item.Address;
                updateStore.City = item.City;
                updateStore.State = item.State;
                updateStore.ZipCode = item.ZipCode;
                updateStore.StorePrice = item.StorePrice;

                db.StoreInfo.Update(updateStore);
                db.SaveChanges();
                Console.WriteLine("Store Updated Successfully");
            }
            else
            {
                Console.WriteLine("Could not update store because it does not exists");
            }
            
        }
        

        public int GetNumItems()
        {
            return db.StoreInfo.Count();
        }
        // /////////////////////////////////////
        public void SetStore(int id, ref StoreInfo st)
        {
            foreach (StoreInfo store in db.StoreInfo)
            {
                if (store.StoreId == id)
                {
                    st = new StoreInfo()
                    {
                        StoreId = store.StoreId,
                        StoreName = store.StoreName,
                        Address = store.Address,
                        City = store.City,
                        State = store.State,
                        ZipCode = store.ZipCode,
                        StorePrice = store.StorePrice
                    };
                    return;
                }
            }
            st = null;
        }

        public bool FindStore(int id)
        {
            foreach (StoreInfo store in db.StoreInfo)
            {
                if (store.StoreId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetStoreName(int id)
        {
            foreach (StoreInfo store in db.StoreInfo)
            {
                if (store.StoreId == id)
                {
                    return store.StoreName;
                }
            }
            return "";
        }
    }
}
