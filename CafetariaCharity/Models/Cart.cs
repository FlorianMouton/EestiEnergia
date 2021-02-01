using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafetariaCharity.Models
{
    public class Cart
    {
        public List<Product> Content { get; set; }
        public int Cost { get; set; }

        public Cart()
        {
            Content = new List<Product>();
        }

        public int AddProductToCart(Product product)
        {
            if (!product.InStock())
            {
                return -1;
            }

            int stock = product.AddProduct();
            this.Content.Add(product);
            this.Cost += product.Price;

            return stock;
        }

        public bool ResetCart()
        {
            if(Content.Count() > 0)
            {
                foreach(Product p in Content)
                {
                    p.RemoveProduct();
                }
                Content.Clear();
                this.Cost = 0;
                return true;
            }
            return false;
        }

        public void PaidCart()
        {            
            Content.Clear();
            Cost = 0;
            return;
        }
    }
}
