﻿using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IProductRepository
    {
        public int Addproduct(Products product);
        public List<Products> GetAllProduct();
    }
}
