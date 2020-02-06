﻿using System;
using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using System.Linq;

namespace PizzaBox.Storing.Repositories
{
    public class RepositoryStorePresetPizzas : IStorePresetPizzas
    {
        PizzaDBContext db;

        public RepositoryStorePresetPizzas()
        {
            db = new PizzaDBContext();
        }
        public RepositoryStorePresetPizzas(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StorePresetPizzas item)
        {
            //We need to see if the store id and order id exist
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId) && db.PresetPizzas.Any(e => e.PizzaName == item.PizzaName))
            {
                db.StorePresetPizzas.Add(item);
                db.SaveChanges();
                Console.WriteLine("preset pizza added successfully");
               
                
            }
            else
            {
                Console.WriteLine("StoreID or Pizza Name not found");
            }
           
        }

        public IEnumerable<StorePresetPizzas> GetItems()
        {
            var query = from e in db.StorePresetPizzas
                        select e;
            return query;
        }

        public void Modify(StorePresetPizzas item)
        {
            throw new NotImplementedException();
        }

        public int GetNumItems()
        {
            return db.StorePresetPizzas.Count();
        }

        // //////////////////////////////////////////

        public IEnumerable<StorePresetPizzas> GetStorePizzas(int id)
        {
            var query = from e in db.StorePresetPizzas
                        where e.StoreId == id
                        select e;
            return query;
        }
    }
}
