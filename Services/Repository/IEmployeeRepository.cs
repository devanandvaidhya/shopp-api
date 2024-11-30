using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IEmployeeRepository
    {
        public Task<int> AddEmployee(Employee employee);
        public Task<int> AddEmpDetails(Employee employee);
        public Task<int> AddProductMapping(Mapping employee);
        public Task<int> AddUserImage(ImageUser employee);
        public Task<int> UpdateEmployee(Employee employee);
        public Task<List<Employee>> GetEmployeeList();
        public Task<List<EmpRoduct>> GetProductList();
        public Task<List<DropDown>> GetStateList();
        public Task<List<DropDown>> GetEmpdrpList();
        public Task<List<DropDown>> GetDistByState( int Id);
        public Task<SearchFilters> GetEmployeeListFilter(SearchFilters searrchfilter);
        public Task<Employee> GetEmployeeById(int Id);
        public Task<Mapping> GetProductMappingById(int Id);
        public Task<int> DeleteEmployeeById(int Id);
        public Task<Employee> UserAuthentication(Login login);
    }
}
