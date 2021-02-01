using CafetariaCharity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CafetariaCharity.Controllers
{
    public class HomeController : Controller
    {

        public const string SessionInfo_Cart = "_Cart";

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetComplexData(SessionInfo_Cart, new Cart());
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public double AddProductToCart(int product)
        {
            Cart cart = HttpContext.Session.GetComplexData<Cart>(SessionInfo_Cart);
            Product p = new Product();
            p.GetProduct(Convert.ToInt32(product));
            int stockLeft = cart.AddProductToCart(p);
            if(stockLeft >= 0)
                HttpContext.Session.SetComplexData(SessionInfo_Cart, cart);

            return stockLeft;
        }

        [HttpPost]
        public void Reset()
        {
            Cart cart = HttpContext.Session.GetComplexData<Cart>(SessionInfo_Cart);

            cart.ResetCart();
            HttpContext.Session.SetComplexData(SessionInfo_Cart, cart);
                        
            return;
        }

        [HttpGet]
        public string Checkout(double amount, double cost)
        {
            string change = string.Empty;

            double changeValue = Convert.ToDouble(amount) - Convert.ToDouble(cost);
            if(changeValue < 0)
            {
                change = "Not enough money inserted";
            }
            else if(changeValue == 0)
            {
                change = "Perfect amount of money inserted";
            }

            else
            {
                change = SmallestChange(changeValue);
               
                Cart cart = HttpContext.Session.GetComplexData<Cart>(SessionInfo_Cart);

                cart.PaidCart();
                HttpContext.Session.SetComplexData(SessionInfo_Cart, cart);
                
                
            }
            return change;
        }

        public string SmallestChange(double changeValue)
        {
            string change = string.Empty;
            //using cents
            List<double> listNotesAvailable = new List<double> { 10000, 5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5, 2, 1 };
            List<double> listChangeAmounts = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < listNotesAvailable.Count(); i++)
            {
                listChangeAmounts[i] = Math.Truncate(changeValue / listNotesAvailable[i]);
                changeValue = changeValue % listNotesAvailable[i];
                if (listChangeAmounts[i] > 0)
                    change += listChangeAmounts[i] + "x" + listNotesAvailable[i]/100 + "€  ";
            }

            return change;
        }

        [HttpGet]
        public JsonResult GetStocks()
        {
            Products listProducts = new Products();
            listProducts.Read();

            return Json(listProducts);
        }

        [HttpGet]
        public int GetTotalPrice()
        {
            Cart cart = HttpContext.Session.GetComplexData<Cart>(SessionInfo_Cart);

            return cart.Cost;
        }
        
    }
}
