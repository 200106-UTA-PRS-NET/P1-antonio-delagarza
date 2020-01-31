using System;
using Xunit;
using PizzaBox.Domain.Models;

namespace PizzaBoxWebApp.Test
{
    
    public class UnitTest1
    {
        Users user = new Users();
        Pizzas pizza = new Pizzas();
        [Fact]
        public void AssignName()
        {
            //Arrange
            string email = "test@gmail.com";
            //Act
            user.Email = email;
            //Assert
            Assert.Equal(user.Email, email);
        }

        [Fact]
        public void CalculatePizzaPrice()
        {
            //Arrange
            pizza = new Pizzas()
            {
                PizzaId = 1,
                Size = "Large",
                Crust = "Thin",
                CrustFlavor = "None",

            };
            //Act
            
        }
        
    }
}
