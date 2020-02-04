using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;
using PizzaBoxWebApp.Models;


namespace PizzaBoxWebApp.Controllers
{
    public class SignedInController : Controller
    {

        RepositoryUsers repositoryUsers = new RepositoryUsers();
        RepositoryStoreInfo repositoryStoreInfo = new RepositoryStoreInfo();
        RepositoryOrdersUserInfo repositoryOrdersUserInfo = new RepositoryOrdersUserInfo();
        RepositoryOrdersPizzaInfo repositoryOrdersPizzaInfo = new RepositoryOrdersPizzaInfo();
        RepositoryPizzas repositoryPizzas = new RepositoryPizzas();
        RepositoryStoreOrdersInfo repositoryStoreOrdersInfo = new RepositoryStoreOrdersInfo();
        RepositoryPresetPizzas repositoryPresetPizzas = new RepositoryPresetPizzas();
        RepositoryStorePresetPizzas repositoryStorePresetPizzas = new RepositoryStorePresetPizzas();

       
        
        
        static decimal calculatePrice(Pizzas p, decimal price)
        {
            decimal pizzaPrice = price;
            //check crust
            if (p.Size == "Small")
            {
                pizzaPrice -= 1M;
            }
            else if (p.Size == "Large")
            {
                pizzaPrice += 1M;
            }
            else if (p.Size == "Extra_Large")
            {
                pizzaPrice += 2M;
            }
            //crust selected
            if (p.Crust == "Original")
            {
                pizzaPrice += 1M;
            }
            else if (p.Crust == "Stuffed")
            {
                pizzaPrice += 2M;
            }
            //Sauce Amount
            if (p.SauceAmount == "Extra")
            {
                pizzaPrice += 2M;
            }
            //Cheese Amount
            if (p.CheeseAmount == "Extra")
            {
                pizzaPrice += 2M;
            }
            //Topping2
            if (p.Topping2 != null)
            {
                pizzaPrice += 0.50M;
            }
            //Topping3
            if (p.Topping3 != null)
            {
                pizzaPrice += 0.50M;
            }


            return pizzaPrice;


        }
        public IActionResult Index()
        {
            TempData["storeId"] = null;
            TempData["Total"] = null;
            TempData["orderId"] = null;
            PizzaList.manyPizzas.Clear();
            string email = TempData["UserEmail"] as string;
            string password = TempData["UserPassword"] as string;
            
            TempData.Keep();
            if (email != null && password != null)
            {
                return View();
            }
            TempData["UserEmail"] = null;
            TempData["UserPassword"] = null;
            return RedirectToAction(nameof(Index), "Home");
        }

        //Place an order
       
            //this will actually select the store
        public IActionResult PlaceOrder()
        {
            return View();
        }
        //////////////////////////////
        #region OrderActions
        [HttpPost]
        public IActionResult PlaceOrder(StoreInfo st)
        {
            //add the order to the database
            TempData["storeId"] = st.StoreId;


            var storeOrders = repositoryStoreOrdersInfo.GetStoreOrders(Convert.ToInt32(TempData["storeId"])).ToList();
            if (storeOrders.Count() != 0)
            {

                var userPurchases = repositoryOrdersUserInfo.GetUserPurchases(TempData["UserEmail"] as string).ToList();

                if (userPurchases.Count() != 0)
                {
                    var joinedTables = (from e in storeOrders
                                        join f in userPurchases
                                        on e.OrderId equals f.OrderId
                                        orderby f.OrderDateTime descending
                                        select f.OrderDateTime).ToList();

                    DateTime now = DateTime.Now;
                    if (joinedTables.Count() != 0)
                    {
                        var o = joinedTables.First();
                        int hoursPassed = o.Subtract(now).Hours;
                        if (hoursPassed < 24)
                        {
                            return View("OrderRestriction");
                        }
                    }

                }
            }
            string total = "0.0000";
            TempData["Total"] = total;
            OrdersUserInfo ordersUserInfo = new OrdersUserInfo()
            {
                Email = TempData["userEmail"] as string,
                OrderDateTime = DateTime.Now
            };
            repositoryOrdersUserInfo.Add(ordersUserInfo);
            StoreOrdersInfo storeOrdersInfo = new StoreOrdersInfo()
            {
                StoreId = st.StoreId,
                OrderId = ordersUserInfo.OrderId

            };
            repositoryStoreOrdersInfo.Add(storeOrdersInfo);
            TempData["orderId"] = ordersUserInfo.OrderId;
            
            TempData.Keep();
            
            return RedirectToAction(nameof(SelectPizza), "SignedIn");
        }

        [Route("/SignedIn/PlaceOrder/SelectPizza")]
        public IActionResult SelectPizza()
        {
            
            return View();
        }

       
        [HttpGet]
        public IActionResult CreatePizza(CombinedPizzasViewModel cp)
        {
            
            if (cp.selectedPizza != "Custom")
            {
                PresetPizzas preset = repositoryPresetPizzas.GetPizza(cp.selectedPizza);
                Pizzas p = new Pizzas()
                {
                    Size = preset.Size,
                    Crust = preset.Crust,
                    CrustFlavor = preset.CrustFlavor,
                    Sauce = preset.Sauce,
                    SauceAmount = preset.SauceAmount,
                    CheeseAmount = preset.CheeseAmount,
                    Topping1 = preset.Topping1,
                    Topping2 = preset.Topping2,
                    Topping3 = preset.Topping3,
                    Price = preset.Price

                };

                PizzaList.manyPizzas.Add(p);
                
                return RedirectToAction(nameof(AddPresetToOrder));
            }
            else
            {
               
                return RedirectToAction(nameof(CreateCustom));
            }
        }

        
        public IActionResult AddPresetToOrder()
        {
            return RedirectToAction(nameof(KeepOrdering));
        }
       
        public IActionResult CreateCustom()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateCustomPizza(Pizzas pz)
        {
            if (pz.Topping1 == "")
            {
                pz.Topping1 = null;
            }
            if (pz.Topping2 == "")
            {
                pz.Topping2 = null;
            }
            if (pz.Topping3 == "")
            {
                pz.Topping3 = null;
            }
            repositoryPizzas.Add(pz);
            
            StoreInfo store = null;
            repositoryStoreInfo.SetStore(Convert.ToInt32(TempData["storeId"]), ref store);
            decimal pizzaPrice = calculatePrice(pz, store.StorePrice);
            pz.Price = pizzaPrice;
            PizzaList.manyPizzas.Add(pz);

            return RedirectToAction(nameof(KeepOrdering));
        }

        public IActionResult KeepOrdering()
        {
            return View();
        }
        
     

        public IActionResult SubmitOrder()
        {
            
            foreach(Pizzas p in PizzaList.manyPizzas)
            {
                repositoryPizzas.Add(p);
                OrdersPizzaInfo ordersPizza = new OrdersPizzaInfo()
                {
                    OrderId = Convert.ToInt32(TempData["orderId"]),
                    PizzaId = p.PizzaId
                };
                repositoryOrdersPizzaInfo.Add(ordersPizza);
            }
            return View("OrderDetails");
           
            
        }
        #endregion
       ////////////////////////////////////////////////////////////////
        //Purchase History
        public IActionResult PurchaseHistory()
        {

            return View();
        }

        
        public IActionResult UpdateProfile()
        {
            return View();
        }

        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction(nameof(Index), "Home");
        }


    }
}