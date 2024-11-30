using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebAPI.MockData
{
    public static class Data
    {
        public static List<Employee> GetSampleEmployee()
        {
            List<Employee> output = new List<Employee>
        {
            new Employee
            {
                FirstName = "Jhon",
                LastName = "Doe",
                Graduation = "BCom",
                DOB = DateTime.Now,
                Email = "jhon@gmail.com",
                Id = 1
            },
            new Employee
            {
                FirstName = "Jhon1",
                LastName = "Doe1",
                Graduation = "Bsc",
                DOB = DateTime.Now,
                Email = "jhon@gmail.com",
                Id = 4
            },
            new Employee
            {
                FirstName = "Jhon2",
                LastName = "Doe2",
                Graduation = "BCA",
                DOB = DateTime.Now,
                Email = "jhon2@gmail.com",
                Id = 5
            }
        };
            return output;
        }
        public static List<Employee> GetEmptyEmployee()
        {
            List<Employee> output = new List<Employee>();
            return output;
        }
    }


}
