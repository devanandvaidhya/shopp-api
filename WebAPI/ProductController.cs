using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _ProductRepository;
        private readonly IProductRepository _ProductRepository1;
        public ProductController(IProductRepository ProductRepository , IProductRepository ProductRepository1) 
        {
            // _ProductRepository = new ProductRepository();
            _ProductRepository = ProductRepository;
            _ProductRepository1 = ProductRepository1;
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Products products)
        {
            _ProductRepository.Addproduct(products);
            var prodList = _ProductRepository1.GetAllProduct();
            return Ok(prodList);
        }
    }
}
