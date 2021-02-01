using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafetariaCharity.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        public Product()
        {

        }

        public void GetProduct(int ID)
        {
            using (var context = new CafetariaCharityContext())
            {
                var query = from st in context.Products
                            where st.ID == ID
                            select st;

                Product p = query.FirstOrDefault<Product>();

                this.ID = p.ID;
                this.Name = p.Name;
                this.Stock = p.Stock;
                this.Price = p.Price;
            }
        }

        public bool InStock()
        {
            bool res = false;

            using (var context = new CafetariaCharityContext())
            {
                var query = from st in context.Products
                            where st.ID == this.ID
                            select st.Stock;

                if (query.FirstOrDefault<int>() > 0)
                    res = true;
            }

            return res;
        }

        //Add a product to the cart 
        public int AddProduct()
        {
            using (var context = new CafetariaCharityContext())
            {
                var query = from st in context.Products
                            where st.ID == this.ID
                            select st;

                Product p = query.FirstOrDefault();
                p.Stock -= 1;
                context.SaveChanges();
                return p.Stock;
            }
        }

        //Remove a product from the cart
        public int RemoveProduct()
        {
            using (var context = new CafetariaCharityContext())
            {
                var query = from st in context.Products
                            where st.ID == this.ID
                            select st;

                Product p = query.FirstOrDefault();
                p.Stock += 1;
                context.SaveChanges();
                return p.Stock;
            }
        }
    }

    public class Products : List<Product>
    {
        public void Read()
        {
            using (var context = new CafetariaCharityContext())
            {
                var query = from st in context.Products
                            select st;

                foreach(Product p in query.ToList())
                {
                    this.Add(p);
                }
                
               
            }
        }
    }
}
