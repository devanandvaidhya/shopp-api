using Dapper;
using Services.Common;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<int> AddEmployee(Employee emp)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@FirstName", emp.FirstName);
                dynamicParameters.Add("@LastName", emp.LastName);
                dynamicParameters.Add("@Email", emp.Email);
                dynamicParameters.Add("@Password", emp.Password);
                dynamicParameters.Add("@UserName", emp.UserName);
                var data = await connection.QueryAsync<int>("SaveEmpoyee", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

        public async Task<int> AddEmpDetails(Employee emp)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", emp.Id);
                dynamicParameters.Add("@Gender", emp.Gender);
                dynamicParameters.Add("@DOB", emp.DOB);
                dynamicParameters.Add("@Hobbies", emp.Hobbies);
                dynamicParameters.Add("@Graduation", emp.Graduation);
                dynamicParameters.Add("@PostGraduation", emp.PostGraduation);
                dynamicParameters.Add("@Address", emp.Address);
                dynamicParameters.Add("@Pincode", emp.Pincode);
                dynamicParameters.Add("@IsEducated", emp.IsEducated);
                var data = await connection.QueryAsync<int>("SaveEmpDetails", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }


        public async Task<int> AddProductMapping(Mapping emp)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmpId", emp.EmpId);
                dynamicParameters.Add("@DistId", emp.DistId);
                dynamicParameters.Add("@stateId", emp.stateId);
                var data = await connection.QueryAsync<int>("SaveProductAreaMapping", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                    foreach (var item in emp.prodIds)
                    {
                        DynamicParameters productdynamicParameters = new DynamicParameters();
                        productdynamicParameters.Add("@productId", item.productId);
                        productdynamicParameters.Add("@MappingId", result);
                        var datas = await connection.QueryAsync<int>("SaveProductMapping", productdynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    }
                    


                }
            }
            return result;
        }

        public async Task<int> AddUserImage(ImageUser emp)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", emp.UserId);
                dynamicParameters.Add("@FilePath", emp.FilePath);
                var data = await connection.QueryAsync<int>("SaveUserImage", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }


        
        public async Task<int> UpdateEmployee(Employee emp)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", emp.Id);
                dynamicParameters.Add("@FirstName", emp.FirstName);
                dynamicParameters.Add("@LastName", emp.LastName);
                dynamicParameters.Add("@Email", emp.Email);
                dynamicParameters.Add("@Password", emp.Password);
                dynamicParameters.Add("@UserName", emp.UserName);
                
                var data = await connection.QueryAsync<int>("UpdateEmployee", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }


        public async Task<int> DeleteEmployeeById(int Id)
        {
            try
            {
                int EmpId = 0;
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Id);
                    EmpId = (await connection.QueryAsync<int>("DeleteEmployeeById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                    return EmpId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployeeById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    Employee EmpList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@id", Id);
                    EmpList = (await connection.QueryAsync<Employee>("GetEmployeeById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                    
                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Mapping> GetProductMappingById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    Mapping EmpList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@empId", Id);
                    EmpList = (await connection.QueryAsync<Mapping>("GetProductAreaMappingByEMpId", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Employee>> GetEmployeeList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Employee> EmpList;

                    EmpList = (await connection.QueryAsync<Employee>("GetEmpList", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                    
                  
                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmpRoduct>> GetProductList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<EmpRoduct> EmpList;

                    EmpList = (await connection.QueryAsync<EmpRoduct>("GetProductList", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DropDown>> GetStateList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<DropDown> EmpList;

                    EmpList = (await connection.QueryAsync<DropDown>("GetState", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DropDown>> GetEmpdrpList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<DropDown> EmpList;

                    EmpList = (await connection.QueryAsync<DropDown>("GetEmpdrp", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DropDown>> GetDistByState(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<DropDown> EmpList;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Id);
                    EmpList = (await connection.QueryAsync<DropDown>("GetDistByState", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<SearchFilters> GetEmployeeListFilter(SearchFilters serachfilter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Employee> EmpList = new List<Employee>();
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    SearchFilters result = new SearchFilters();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@SearchTerm", serachfilter.SearchFilter);
                    dynamicParameters.Add("@Page", serachfilter.PageNumber);
                    dynamicParameters.Add("@Size", serachfilter.PageSize);
                    //dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var reader = await connection.QueryMultipleAsync("GetAllEmpByPagination", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    EmpList = reader.Read<Employee>().ToList();
                    result.TotalCount = dynamicParameters.Get<int>("RecordCount");
                    result.employee = EmpList;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> UserAuthentication(Login login)
        {
            Employee result = new Employee();
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", login.UserName);
                dynamicParameters.Add("@Password", login.Password);
                var data = await connection.QueryAsync<Employee>("UserAuthentication", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }
    }
}
