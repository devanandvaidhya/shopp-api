using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Models;
using Services.Repository;
using Services.Repository.SubscriberOperation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPI.ResponseFormat;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentRepository _StudentRepository;
        private readonly IStockObserable _StockObserable;
        private IConfiguration _config;
        public StudentController(IStudentRepository studentRepository, IConfiguration config, IStockObserable stockObserable)
        {
            _StudentRepository = studentRepository;
            _config = config;
            _StockObserable = stockObserable;
        }

        [HttpPost("SaveStudent")]
        public async Task<ResultEntity<int>> SaveStudent(Student student)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _StudentRepository.AddStudent(student);
            result.Message = "Record has been saved successfully!";
            return result;
        }

        [HttpGet("GetStudentList")]
        public async Task<ResultEntity<List<Student>>> GetStudentList()
        {
            ResultEntity<List<Student>> resultEntity = new ResultEntity<List<Student>>();
            resultEntity.Entity = await _StudentRepository.GetStudent();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<ResultEntity<Student>> GetStudentById(int Id)
        {
            ResultEntity<Student> resultEntity = new ResultEntity<Student>();
            resultEntity.Entity = await _StudentRepository.GetStudentById(Id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpDelete("DeleteStudentById/{id}")]
        public async Task<ResultEntity<int>> DeleteStudentById(int Id)
        {
            ResultEntity<int> resultEntity = new ResultEntity<int>();
            resultEntity.Entity = await _StudentRepository.DeleteStudentById(Id);
            resultEntity.Message = "Record Deleted successfully!.";
            return resultEntity;
        }


        [HttpPost("UpdateStudent")]
        public async Task<ResultEntity<int>> UpdateStudent(Student student)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _StudentRepository.UpdateStudent(student);
            result.Message = "Record has been Updated successfully!";
            return result;
        }


        [HttpPost("StudentAuthentication")]
        public async Task<ResultEntity<Student>> UserAuthentication(Login login)
        {
            ResultEntity<Student> resultEnitity = new ResultEntity<Student>();

            var obj = await _StudentRepository.UserAuthentication(login);
            if (obj != null)
            {

                resultEnitity.Entity = obj;
                if (resultEnitity.Entity.ProfilePath != null)
                {
                    resultEnitity.Entity.ProfileFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + resultEnitity.Entity.ProfilePath);
                }
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

        [HttpGet("IsStudentExists/{Email}")]
        public async Task<ResultEntity<Student>> IsStudentExists(string Email)
        {
            ResultEntity<Student> resultEnitity = new ResultEntity<Student>();

            var obj = await _StudentRepository.IsStudentExists(Email);
            if (obj != null)
            {

                resultEnitity.Entity = obj;

                resultEnitity.Message = "Student is exists";
                resultEnitity.Entity.IsAuthenticate = true;
            }
            else
            {
                resultEnitity.Message = "Student not exists";
                resultEnitity.Entity.IsAuthenticate = false;
            }
            return resultEnitity;


        }


        [HttpGet("GetCategories")]
        public async Task<ResultEntity<List<Category>>> GetCategories()
        {
            ResultEntity<List<Category>> resultEntity = new ResultEntity<List<Category>>();
            resultEntity.Entity = await _StudentRepository.GetCategories();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpPost("SaveProduct"), DisableRequestSizeLimit]
        public async Task<ResultEntity<int>> SaveProduct([FromForm] Product product )
        {
            ResultEntity<int> result = new ResultEntity<int>();
            try
            {
                var file = product.File;// Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Products");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                product.productFeatures = JsonConvert.DeserializeObject<List<ProductFeatures>>(product.ProductFeature);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, product.CategoryId.ToString() + "_" + fileName);
                    var dbPath = Path.Combine(folderName, product.CategoryId.ToString() + "_" + fileName);
                    //Product ImgObj = new Product();
                    //ImgObj.CategoryId = data.CategoryId;
                    // ImgObj.FilePath = dbPath;
                    product.FilePath = dbPath;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    result.Entity = await _StudentRepository.AddProduct(product);
                    result.Message = "Product added sucessfull!.";
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

        [HttpGet("GetProductlist")]
        public async Task<ResultEntity<List<Product>>> GetProductlist()
        {
           // SearchFilters obj = new SearchFilters();
            //obj.PageSize = 5;
           // obj.PageNumber = PageNumber;
            ResultEntity<List<Product>> resultEntity = new ResultEntity<List<Product>>();
            resultEntity.Entity = await _StudentRepository.GetProductList();

            foreach (var item in resultEntity.Entity)
            {
                if (item.ProductPath != null)
                {
                    //Byte[] b;
                    item.ProductFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + item.ProductPath);
                }

            }


            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpGet("GetProductByPagination/{PageNumber}/{PageSize}")]
        public async Task<ResultEntity<SearchProduct>> GetEmployeeByPagination(int PageNumber, int PageSize)
        {
            SearchProduct obj = new SearchProduct();
            obj.PageSize = PageSize;
            obj.PageNumber = PageNumber;
            ResultEntity<SearchProduct> resultEntity = new ResultEntity<SearchProduct>();
            resultEntity.Entity = await _StudentRepository.GetProductListFilter(obj);

            foreach (var item in resultEntity.Entity.product)
            {
                if (item.ProductPath != null)
                {
                    //Byte[] b;
                    item.ProductFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + item.ProductPath);
                }

            }


            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpGet("GetUserByPagination/{PageNumber}/{PageSize}")]
        public async Task<ResultEntity<SearchUser>> GetUserByPagination(int PageNumber, int PageSize)
        {
            SearchUser obj = new SearchUser();
            obj.PageSize = PageSize;
            obj.PageNumber = PageNumber;
            ResultEntity<SearchUser> resultEntity = new ResultEntity<SearchUser>();
            resultEntity.Entity = await _StudentRepository.GetUsersListFilter(obj);

            foreach (var item in resultEntity.Entity.Users)
            {
                if (item.ProfilePath != null)
                {
                    //Byte[] b;
                    item.ProfileFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + item.ProfilePath);
                    item.ProfilePath = "";
                }

            }


            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetProductById/{ProductId}")]
        public async Task<ResultEntity<Product>> GetProductById(int ProductId)
        {
            ResultEntity<Product> resultEntity = new ResultEntity<Product>();
            resultEntity.Entity = await _StudentRepository.GetProductById(ProductId);
            if (resultEntity.Entity.ProductPath != null)
            {
                resultEntity.Entity.ProductFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + resultEntity.Entity.ProductPath);
            }
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpGet("GetEmail/{UserId}")]
        public async Task<ResultEntity<List<UserEmail>>> GetEmail(int UserId)
        {
            ResultEntity<List<UserEmail>> resultEntity = new ResultEntity<List<UserEmail>>();
            resultEntity.Entity = await _StudentRepository.GetEmailByUserId(UserId);
            
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }




        [HttpPost("DeleteEmails")]
        public async Task<ResultEntity<int>> DeleteEmails(List<UserEmail> EmailIds)
        {
            ResultEntity<int> resultEntity = new ResultEntity<int>();
           //var result = await _StudentRepository.DeleteEmails(MailIds);
            //resultEntity.Entity = await _StudentRepository.DeleteEmails(MailIds);

            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetEmailDetails/{MailId}")]
        public async Task<ResultEntity<UserEmail>> GetEmailDetails(int MailId)
        {
            ResultEntity<UserEmail> resultEntity = new ResultEntity<UserEmail>();
            resultEntity.Entity = await _StudentRepository.GetEmailDetails(MailId);

            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpPost("SaveProductComment")]
        public async Task<ResultEntity<int>> SaveProductComment(ProductComment comment)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _StudentRepository.SaveProductComment(comment);
            result.Message = "Record has been saved successfully!";
            return result;
        }


        [HttpPost("AddUserNotifyyme/{ProductId}/{emailId}/{id}")]
        public async Task<ResultEntity<int>> AddUserNotifyyme(int ProductId, string emailId, int id)
        {
            NotificationAlertObserver subscriber = new EmailNotification(productId: ProductId, emailId: emailId, userId: id);
            _StockObserable.Add(subscriber);

            ResultEntity<int> result = new ResultEntity<int>();
           // result.Entity = await _StudentRepository.AddUserNotyme(ProductId, emailId, id);
            result.Message = "Record has been saved successfully!";
            return result;
        }

        [HttpPost("RemoveUserNotifyyme/{ProductId}/{emailId}/{id}")]
        public async Task<ResultEntity<int>> RemoveUserNotifyyme(int ProductId, string emailId, int id)
        {
            NotificationAlertObserver subscriber = new EmailNotification(productId: ProductId, emailId: emailId, userId: id);
            _StockObserable.Remove(subscriber);

            ResultEntity<int> result = new ResultEntity<int>();
            // result.Entity = await _StudentRepository.AddUserNotyme(ProductId, emailId, id);
            result.Message = "Record has been saved successfully!";
            return result;
        }

        [HttpPost("AddStock/{CurrentProductId}/{StockRange}")]
        public async Task<ResultEntity<int>> AddStock(int CurrentProductId, int StockRange)
        {
            //NotificationAlertObserver subscriber = new EmailNotification(productId: ProductId, emailId: emailId, userId: id);
            _StockObserable.setStockCount(StockRange,CurrentProductId);

            ResultEntity<int> result = new ResultEntity<int>();
            // result.Entity = await _StudentRepository.AddUserNotyme(ProductId, emailId, id);
            result.Message = "Record has been saved successfully!";
            return result;
        }


        [HttpGet("GetProductComment/{ProductId}")]
        public async Task<ResultEntity<List<ProductComment>>> GetProductComment(int ProductId)
        {
            ResultEntity<List<ProductComment>> resultEntity = new ResultEntity<List<ProductComment>>();
            resultEntity.Entity = await _StudentRepository.GetProductComment(ProductId);
           
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


        [HttpPost("UpdateUserProfile"), DisableRequestSizeLimit]
        public async Task<ResultEntity<int>> UpdateUserProfile([FromForm] UserProfile UserProfile)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            try
            {
                var file = UserProfile.File;// Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "UserProfile");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, UserProfile.Id.ToString() + "_" + fileName);
                    var dbPath = Path.Combine(folderName, UserProfile.Id.ToString() + "_" + fileName);
                    //Product ImgObj = new Product();
                    //ImgObj.CategoryId = data.CategoryId;
                    // ImgObj.FilePath = dbPath;
                    UserProfile.FilePath = dbPath;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    result.Entity = await _StudentRepository.UpdateUserProfile(UserProfile);
                    result.Message = "User profile added sucessfull!.";
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


        [HttpPost("SendRequest/{Id}")]
        public async Task<ResultEntity<int>> SendRequest(int Id)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _StudentRepository.SendRequest(Id);
            result.Message = "Record has been Updated successfully!";
            return result;
        }

        [HttpPost("UpdateStatus/{Id}/{StatusId}/{Comment}")]
        public async Task<ResultEntity<int>> UpdateStatus(int Id,int StatusId,string Comment)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _StudentRepository.UpdateStatus(Id, StatusId, Comment);
            result.Message = "Record has been Updated successfully!";
            return result;
        }

        [HttpGet("GetProdutFeatureById/{ProductId}")]
        public async Task<ResultEntity<List<ProductFeatures>>> GetProdutFeatureById(int ProductId)
        {
            ResultEntity<List<ProductFeatures>> resultEntity = new ResultEntity<List<ProductFeatures>>();
            resultEntity.Entity = await _StudentRepository.GetProdutFeatureById(ProductId);

            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetStateList")]
        public async Task<ResultEntity<List<DropDown>>> GetStateList()
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _StudentRepository.GetStateList();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetDistByState/{id}")]
        public async Task<ResultEntity<List<DropDown>>> GetDistByState(int id)
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _StudentRepository.GetDistByState(id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetCitytByDistrict/{id}")]
        public async Task<ResultEntity<List<DropDown>>> GetCitytByDistrict(int id)
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _StudentRepository.GetCitytByDistrict(id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetBankList")]
        public async Task<ResultEntity<List<DropDown>>> GetBankList()
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _StudentRepository.GetBankList();
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetIFSCByBank/{id}")]
        public async Task<ResultEntity<List<DropDown>>> GetIFSCByBank(int id)
        {
            ResultEntity<List<DropDown>> resultEntity = new ResultEntity<List<DropDown>>();
            resultEntity.Entity = await _StudentRepository.GetIFSCByBank(id);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpPost("PaymentProcess")]
        public async Task<ResultEntity<PaymentSuccess>> PaymentProcess(Payment payment)
        {
            ResultEntity<PaymentSuccess> resultEnitity = new ResultEntity<PaymentSuccess>();

            var obj = await _StudentRepository.PaymentProcess(payment);
            if (obj != null)
            {

                resultEnitity.Entity = obj;
                resultEnitity.Entity.IsSuccess = true;
            }
            else
            {
                resultEnitity.Message = "Please enter correct details!";
                resultEnitity.Entity.IsSuccess = false;
            }
            return resultEnitity;


        }

        [HttpPost("OrderProcess")]
        public async Task<ResultEntity<Order>> OrderProcess(Order Order)
        {
            ResultEntity<Order> resultEnitity = new ResultEntity<Order>();

            var obj = await _StudentRepository.OrderProcess(Order);
            if (obj != null)
            {

                resultEnitity.Entity = obj;
                resultEnitity.Entity.IsSuccess = true;
            }
            else
            {
                resultEnitity.Message = "Order has not been processed!";
                resultEnitity.Entity.IsSuccess = false;
            }
            return resultEnitity;


        }

        [HttpGet("GetOrderByOrderId/{OrderId}")]
        public async Task<ResultEntity<OrderDetails>> GetOrderByOrderId(string OrderId)
        {
            ResultEntity<OrderDetails> resultEntity = new ResultEntity<OrderDetails>();
            resultEntity.Entity = await _StudentRepository.GetOrderByOrderId(OrderId);
            if (resultEntity.Entity.ProductPath != null)
            {
                resultEntity.Entity.ProductFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + resultEntity.Entity.ProductPath);
            }
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpPost("AddToCart")]
        public async Task<ResultEntity<int>> AddToCart(Order order)
        {
            ResultEntity<int> result = new ResultEntity<int>();
            result.Entity = await _StudentRepository.AddToCart(order);
            result.Message = "Record has been saved successfully!";
            return result;
        }

        [HttpGet("GetCartProduct/{PageNumber}/{PageSize}/{UserId}")]
        public async Task<ResultEntity<SearchProduct>> GetCartProduct(int PageNumber, int PageSize,int UserId)
        {
            SearchProduct obj = new SearchProduct();
            obj.PageSize = PageSize;
            obj.PageNumber = PageNumber;
            obj.UserId = UserId;
            ResultEntity<SearchProduct> resultEntity = new ResultEntity<SearchProduct>();
            resultEntity.Entity = await _StudentRepository.GetCartProduct(obj);

            foreach (var item in resultEntity.Entity.product)
            {
                if (item.ProductPath != null)
                {
                    //Byte[] b;
                    item.ProductFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + item.ProductPath);
                }

            }


            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetCartItemByUserId/{UserId}")]
        public async Task<ResultEntity<List<Product>>> GetCartItemByUserId(int UserId)
        {
            ResultEntity<List<Product>> resultEntity = new ResultEntity<List<Product>>();
            resultEntity.Entity = await _StudentRepository.GetCartItemByUserId(UserId);

            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetOrderByUserId/{PageNumber}/{PageSize}/{UserId}")]
        public async Task<ResultEntity<SearchOrder>> GetOrderByUserId(int PageNumber, int PageSize, int UserId)
        {
            SearchOrder obj = new SearchOrder();
            obj.PageSize = PageSize;
            obj.PageNumber = PageNumber;
            obj.UserId = UserId;
            ResultEntity<SearchOrder> resultEntity = new ResultEntity<SearchOrder>();
            resultEntity.Entity = await _StudentRepository.GetOrderByUserId(obj);

            foreach (var item in resultEntity.Entity.Orders)
            {
                if (item.ProductPath != null)
                {
                    //Byte[] b;
                    item.ProductFile = System.IO.File.ReadAllBytes("D:\\.Net\\Applicaiton\\React-Web-API\\WebAPI\\WebAPI\\" + item.ProductPath);
                }

            }


            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }

        [HttpGet("GetOrderCount/{Role}/{UserId}")]
        public async Task<ResultEntity<List<OrderCount>>> GetOrderCount(string Role, int UserId)
        {
            ResultEntity<List<OrderCount>> resultEntity = new ResultEntity<List<OrderCount>>();
            resultEntity.Entity = await _StudentRepository.GetOrderCount(Role, UserId);
            resultEntity.Message = "Record fetched successfully!.";
            return resultEntity;
        }


    }
}
