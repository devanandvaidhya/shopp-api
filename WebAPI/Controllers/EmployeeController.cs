using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.ResponseFormat;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IEmployeeRepository _EmployeeRepository;
        private IConfiguration _config;
        public EmployeeController(IEmployeeRepository EmployeeRepository, IConfiguration config)
        {
            _EmployeeRepository = EmployeeRepository;
            _config = config;
        }

        [HttpPost("SaveEmployee")]
        public async Task<ResultEntity<int>> SaveEmployee(Employee employee)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _EmployeeRepository.AddEmployee(employee);
            result.Message = "Record has been saved successfully!";
            return result;
        }


        [HttpPost("SaveEmpDetails")]
        public async Task<ResultEntity<int>> SaveEmpDetails(Employee employee)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _EmployeeRepository.AddEmpDetails(employee);
            result.Message = "Record has been saved successfully!";
            return result;
        }

        [HttpPost("AddMappingProduct")]
        public async Task<ResultEntity<int>> AddMappingProduct(Mapping maping)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _EmployeeRepository.AddProductMapping(maping);
            result.Message = "Record has been saved successfully!";
            return result;
        }

        [HttpPost("UpdateEmployee")]
        public async Task<ResultEntity<int>> UpdateEmployee(Employee employee)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _EmployeeRepository.UpdateEmployee(employee);
            result.Message = "Record has been Updated successfully!";
            return result;
        }

        [HttpGet("GetEmployeeList")]
        public async Task<ResultEntity<List<Employee>>> GetEmployeeList()
        {
            ResultEntity<List<Employee>> resultEntity = new ResultEntity<List<Employee>>();
            resultEntity.Entity = await _EmployeeRepository.GetEmployeeList();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetProductList")]
        public async Task<ResultEntity<List<EmpRoduct>>> GetProductList()
        {
            ResultEntity<List<EmpRoduct>> resultEntity = new ResultEntity<List<EmpRoduct>>();
            resultEntity.Entity = await _EmployeeRepository.GetProductList();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetStateList")]
        public async Task<ResultEntity<List<DropDown>>> GetStateList()
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _EmployeeRepository.GetStateList();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetEmpdrpList")]
        public async Task<ResultEntity<List<DropDown>>> GetEmpdrpList()
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _EmployeeRepository.GetEmpdrpList();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetDistByState/{id}")]
        public async Task<ResultEntity<List<DropDown>>> GetDistByState(int id)
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _EmployeeRepository.GetDistByState(id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpGet("GetEmployeeByPagination/{PageNumber}")]
        public async Task<ResultEntity<SearchFilters>> GetEmployeeByPagination(int PageNumber)
        {
            SearchFilters obj = new SearchFilters();
            obj.PageSize = 5;
            obj.PageNumber = PageNumber;
            ResultEntity<SearchFilters> resultEntity = new ResultEntity<SearchFilters>();
            resultEntity.Entity = await _EmployeeRepository.GetEmployeeListFilter(obj);

            foreach (var item in resultEntity.Entity.employee)
            {
                if (item.FilePath != null)
                {
                    //Byte[] b;
                    item.UserFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\WebAPI\\WebAPI\\" + item.FilePath);
                }
                
            }
           

            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ResultEntity<Employee>> GetEmployeeById(int Id)
        {
            ResultEntity<Employee> resultEntity = new ResultEntity<Employee>();
            resultEntity.Entity = await _EmployeeRepository.GetEmployeeById(Id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetProductByEmpId/{id}")]
        public async Task<ResultEntity<Mapping>> GetProductByEmpId(int Id)
        {
            ResultEntity<Mapping> resultEntity = new ResultEntity<Mapping>();
            resultEntity.Entity = await _EmployeeRepository.GetProductMappingById(Id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpDelete("DeleteEmployeeById/{id}")]
        public async Task<ResultEntity<int>> DeleteEmployeeById(int Id)
        {
            ResultEntity<int> resultEntity = new ResultEntity<int>();
            resultEntity.Entity = await _EmployeeRepository.DeleteEmployeeById(Id);
            resultEntity.Message = "Record Deleted successfully!.";
            return resultEntity;
        }

        [AllowAnonymous]
        [HttpPost("UserAuthentication")]
        public async Task<ResultEntity<Employee>> UserAuthentication(Login login)
        {
            ResultEntity<Employee> resultEnitity = new ResultEntity<Employee>();
            
             var obj = await _EmployeeRepository.UserAuthentication(login);
            if (obj != null)
            {
                
                    var tokenString = GenerateJSONWebToken(obj);
                   

                
                resultEnitity.Entity = obj;
                resultEnitity.Entity.Token = tokenString;
                resultEnitity.Message = "Welcome to " + resultEnitity.Entity.FirstName;
                resultEnitity.Entity.IsAuthenticate = true;
            }
            else
            {
                resultEnitity.Message = "Please enter correct username or password!";
                resultEnitity.Entity.IsAuthenticate = false;
            }
            return resultEnitity;


        }

        [HttpPost("UploadImage"), DisableRequestSizeLimit]
        public async Task<ResultEntity<int>> UploadImage([FromForm] ImageUser data)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            try
            {
                var file = data.File;// Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, data.UserId.ToString() + "_" + fileName);
                    var dbPath = Path.Combine(folderName, data.UserId.ToString()+"_"+fileName);
                    ImageUser ImgObj = new ImageUser();
                    ImgObj.UserId = data.UserId;
                    ImgObj.FilePath = dbPath;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    result.Entity = await _EmployeeRepository.AddUserImage(ImgObj);
                    result.Message = "Image uploaded sucessfull!.";
                    return result;
                }
                else
                {
                    result.Entity = 0;
                    result.Message = "Please select proper image";
                    return result;
                }
            }
            catch (Exception ex)
            {
                 result.Entity = 0;
                result.Message = ex.Message;
                return result;
            }
        }


        private string GenerateJSONWebToken(Employee userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                 };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
