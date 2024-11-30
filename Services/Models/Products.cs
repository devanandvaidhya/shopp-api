using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
    }

    public class EmpRoduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }

    public class DropDown
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class productIds
    {
        public int productId { get; set; }
    }

    public class Mapping
    {
        public List<productIds> prodIds { get; set; }
        public int EmpId { get; set; }
        public int stateId { get; set; }
        public int DistId { get; set; }

        // fetcing product details
        public string StateName { get; set; }         
        public string DistNam { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }

    }
}
