using PizzaBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IOrdersUserInfo : IRepository<OrdersUserInfo>
    {
        public IEnumerable<OrdersUserInfo> GetUserPurchases(string email);
        public OrdersUserInfo GetStoreOrderDetails(int id);
    }
}
