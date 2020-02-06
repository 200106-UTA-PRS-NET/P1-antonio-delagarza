﻿using System;
using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using System.Linq;

namespace PizzaBox.Storing.Repositories
{
    public class RepositoryStoreOrdersInfo : IStoreOrdersInfo
    {

        PizzaDBContext db;

        public RepositoryStoreOrdersInfo()
        {
            db = new PizzaDBContext();
        }
        public RepositoryStoreOrdersInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StoreOrdersInfo item)
        {

            //We need to see if the store id and order id exist
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId) && db.OrdersUserInfo.Any(e => e.OrderId == item.OrderId))
            {
                db.StoreOrdersInfo.Add(item);

                db.SaveChanges();
                
            }
            else
            {
                Console.WriteLine("OrderID or StoreID not found");
            }
            
        }

        public IEnumerable<StoreOrdersInfo> GetItems()
        {
            var query = from e in db.StoreOrdersInfo
                        select e;

            return query;
        }

        public void Modify(StoreOrdersInfo item)
        {
            throw new NotImplementedException();
        }
        public int GetNumItems()
        {
            return db.StoreOrdersInfo.Count();
        }
         // ////////////////////////////////////
        public int GetStoreId(int orderId)
        {
            foreach (StoreOrdersInfo st in db.StoreOrdersInfo)
            {
                if (st.OrderId == orderId)
                {
                    return st.StoreId;
                }
            }
            return -1;
        }

        public IEnumerable<StoreOrdersInfo> GetStoreOrders(int id)
        {
            var query = from e in db.StoreOrdersInfo
                        where e.StoreId == id
                        select e;

            return query;
        }

        
    }
}
