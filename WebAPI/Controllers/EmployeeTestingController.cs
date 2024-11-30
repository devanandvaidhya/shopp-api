using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ResponseFormat;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTestingController : ControllerBase
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        public EmployeeTestingController(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        [HttpGet("GetEmployeeList")]
        public async Task<ResultEntity<List<Employee>>> GetEmployeeList()
        {
            ResultEntity<List<Employee>> resultEntity = new ResultEntity<List<Employee>>();
            resultEntity.Entity = await _EmployeeRepository.GetEmployeeList();
            if (resultEntity.Entity.Count > 0)
            {
                resultEntity.Message = "Record fetched successfully!.";

            }
            else
            { 
            resultEntity.Message = "Data not found";
            }
            return resultEntity;
        }
    }
}
