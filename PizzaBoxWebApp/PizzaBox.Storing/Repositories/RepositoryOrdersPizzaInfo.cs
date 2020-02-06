using System;
using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using System.Linq;

namespace PizzaBox.Storing.Repositories
{
    public class RepositoryOrdersPizzaInfo : IOrdersPizzaInfo
    {
        PizzaDBContext db;

        public RepositoryOrdersPizzaInfo()
        {
            db = new PizzaDBContext();
        }
        public RepositoryOrdersPizzaInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(OrdersPizzaInfo item)
        {
            if (db.Pizzas.Any(e => e.PizzaId == item.PizzaId) && db.OrdersUserInfo.Any(e => e.OrderId == item.OrderId))
            {
                db.OrdersPizzaInfo.Add(item);
                db.SaveChanges();
                
               
            }
            else
            {
                Console.WriteLine("OrderID or PizzaID not found");
            }
            
        }

        public IEnumerable<OrdersPizzaInfo> GetItems()
        {
            var query = from e in db.OrdersPizzaInfo
                        select e;

            return query;
        }

        public void Modify(OrdersPizzaInfo item)
        {
            throw new NotImplementedException();
        }
        public int GetNumItems()
        {
            return db.OrdersPizzaInfo.Count();
        }

        public IEnumerable<OrdersPizzaInfo> GetOrdersPizzas(int id)
        {
            var query = from e in db.OrdersPizzaInfo
                        where e.OrderId == id
                        select e;

            return query;
        }

       
    }
}
