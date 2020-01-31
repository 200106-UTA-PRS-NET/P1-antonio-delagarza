﻿using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class OrdersPizzaInfo
    {
        public int OrderId { get; set; }
        public int PizzaId { get; set; }

        public virtual OrdersUserInfo Order { get; set; }
        public virtual Pizzas Pizza { get; set; }
    }
}
