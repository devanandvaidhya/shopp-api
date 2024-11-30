using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Employee 
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticate { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public DateTime DOB { get; set; }
        public string Hobbies { get; set; }
        public string Graduation { get; set; }
        public string PostGraduation { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public bool IsEducated { get; set; }
        public int Gender { get; set; }
        public string FilePath { get; set; }
        public byte[] UserFile { get; set; }



    }

    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SearchFilters
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchFilter { get; set; }
        public int TotalCount { get; set; }
        public List<Employee> employee { get; set; }
    }

    public class ImageUser
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
    }

}
