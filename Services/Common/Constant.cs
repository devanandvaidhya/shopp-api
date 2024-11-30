using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class Constant
    {
        public static string ProductUrl = "http://localhost:3000/product-details/";
        public static string From = "ECMNOTF";
        public static string Subject = "Product Available";

    }

    public static class commonRepository
    {
        public static string EmpConnection { set; get; }
        
    }


}
