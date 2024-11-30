using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{

    
    public class ProductRepository : IProductRepository
    {
        public List<Products> products = new List<Products>();
        public int Addproduct(Products product)
        {
            product.Id = products.Count + 1;
            products.Add(product);
            return product.Id;
        }

        public List<Products> GetAllProduct()
        {
            return products;
        }
    }
}
