using CafetariaCharity;
using CafetariaCharity.Controllers;
using CafetariaCharity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace EestiEnergia
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Test_SmallestChangeMethod()
        {
            var controller = new HomeController();

            string res = controller.SmallestChange(7);
            string expected = "1x0,05€  1x0,02€  ";
            Assert.AreEqual(expected, res);

        }

        [TestMethod]
        public void Test_CheckoutNegativeMethod()
        {
            var controller = new HomeController();

            string res = controller.Checkout(200, 500);
            string expected = "Not enough money inserted";
            Assert.AreEqual(expected, res);

        }

        [TestMethod]
        public void Test_CheckoutExactMethod()
        {
            var controller = new HomeController();

            string res = controller.Checkout(200, 200);
            string expected = "Perfect amount of money inserted";
            Assert.AreEqual(expected, res);

        }

        [TestMethod]
        public void Test_ResetCartMethod()
        {
            Cart c = new Cart();
            Product p = new Product()
            {
                Name = "Muffin",
                ID = 2,
                Price = 100,
                Stock = 36
            };

            c.Content.Add(p);
            c.Cost = p.Price;

            c.ResetCart();
            
            Assert.AreEqual(0, c.Content.Count);
            Assert.AreEqual(0, c.Cost);


        }

        [TestMethod]
        public void Test_AddProductToCartMethod()
        {
            Cart c = new Cart();
            Product p = new Product()
            {
                Name = "Muffin",
                ID = 2,
                Price = 100,
                Stock = 36
            };

            c.AddProductToCart(p);

            Assert.AreEqual(1, c.Content.Count);
            Assert.AreEqual(100, c.Cost);


        }
        

        [TestMethod]
        public void Test_AddProductMethod()
        {
            Product p = new Product()
            {
                Name = "Brownie",
                ID = 1,
                Price = 60,
                Stock = 48
            };

            int res = p.AddProduct();

            Assert.AreEqual(47, res);
        }

        [TestMethod]
        public void Test_RemoveProductMethod()
        {
            Product p = new Product()
            {
                Name = "Brownie",
                ID = 1,
                Price = 60,
                Stock = 48
            };

            int res = p.RemoveProduct();

            Assert.AreEqual(48, res);
        }

        [TestMethod]
        public void Test_InStockMethod()
        {
            Product p = new Product()
            {
                Name = "Brownie",
                ID = 1,
                Price = 60,
                Stock = 48
            };

            bool res = p.InStock();

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void Test_GetProductMethod()
        {
            Product p = new Product()
            {
                Name = "Brownie",
                ID = 1,
                Price = 60,
                Stock = 48
            };

            Product res = new Product();
            res.GetProduct(1);

            Assert.AreEqual(p.ID, res.ID);
            Assert.AreEqual(p.Name, res.Name);
            Assert.AreEqual(p.Price, res.Price);
            Assert.AreEqual(p.Stock, res.Stock);
        }

        [TestMethod]
        public void Test_PaidCartMethod()
        {
            Cart c = new Cart();
            Product p = new Product()
            {
                Name = "Brownie",
                ID = 1,
                Price = 60,
                Stock = 48
            };

            c.Content.Add(p);
            c.Cost = 60;
            c.PaidCart();

            Assert.AreEqual(0, c.Content.Count);
            Assert.AreEqual(0, c.Cost);
            
        }
    }
}
