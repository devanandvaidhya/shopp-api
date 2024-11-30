using Dapper;
using Services.Common;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Services.Repository
{
    public class StudentRepository : IStudentRepository
    {

        public async Task<int> AddStudent(Student student)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@FirstName", student.FirstName);
                dynamicParameters.Add("@LastName", student.LastName);
                dynamicParameters.Add("@Email", student.EmailId);
                dynamicParameters.Add("@Password", student.Password);
                dynamicParameters.Add("@UserName", student.UserName);
                dynamicParameters.Add("@Gender", student.Gender);
                dynamicParameters.Add("@Language", student.Language);
                dynamicParameters.Add("@DOB", student.DOB);
                try
                {
                    var data = await connection.QueryAsync<int>("SaveStudent", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = data.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                { 
                }
            }
            return result;
        }


        public async Task<Student> GetStudentById(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    Student EmpList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@id", Id);
                    EmpList = (await connection.QueryAsync<Student>("GetstudentById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Student>> GetStudent()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Student> EmpList;

                    EmpList = (await connection.QueryAsync<Student>("GetstudentList", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Product>> GetProductList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Product> EmpList;

                    EmpList = (await connection.QueryAsync<Product>("getProducts", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Category> CatList;

                    CatList = (await connection.QueryAsync<Category>("getCategory", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return CatList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteStudentById(int Id)
        {
            try
            {
                int EmpId = 0;
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Id);
                    EmpId = (await connection.QueryAsync<int>("DeleteStudentById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                    return EmpId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> UpdateStudent(Student student)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", student.Id);
                dynamicParameters.Add("@FirstName", student.FirstName);
                dynamicParameters.Add("@LastName", student.LastName);
                dynamicParameters.Add("@Email", student.EmailId);
                dynamicParameters.Add("@Password", student.Password);
                dynamicParameters.Add("@UserName", student.UserName);
                dynamicParameters.Add("@DOB", student.DOB);
                dynamicParameters.Add("@Languages", student.Language);

                var data = await connection.QueryAsync<int>("UpdateStudent", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }


        public async Task<int> SendRequest(int Id)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", Id);

                var data = await connection.QueryAsync<int>("SendRequest", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

        public async Task<int> UpdateStatus(int Id,int StatusId, string Comment)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", Id);
                dynamicParameters.Add("@StatusId", StatusId);
                dynamicParameters.Add("@Comments", Comment);

                var data = await connection.QueryAsync<int>("UpdateUserStatus", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

        public async Task<Student> UserAuthentication(Login login)
        {
            Student result = new Student();
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", login.UserName);
                dynamicParameters.Add("@Password", login.Password);
                var data = await connection.QueryAsync<Student>("StudentAuthentication", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

        public async Task<PaymentSuccess> PaymentProcess(Payment payment)
        {
            PaymentSuccess result = new PaymentSuccess();
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@BankId", payment.BankId);
                dynamicParameters.Add("@IFSCCode", payment.IFSCCode);
                dynamicParameters.Add("@cardNumber", payment.cardNumber);
                dynamicParameters.Add("@cardHolder", payment.cardHolder);
                dynamicParameters.Add("@expiryMonth", payment.expiryMonth);
                dynamicParameters.Add("@expiryYear", payment.expiryYear);
                dynamicParameters.Add("@cvv", payment.cvv);
                dynamicParameters.Add("@Amount", payment.Amount);
                var data = await connection.QueryAsync<PaymentSuccess>("PaymentProcess", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

        public async Task<Order> OrderProcess(Order order)
        {
            Order result = new Order();
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ProductId", order.ProductId);
                dynamicParameters.Add("@UserId", order.UserId);

                var data = await connection.QueryAsync<Order>("PlaceOrder", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();

                    DynamicParameters AddrdynamicParameters = new DynamicParameters();
                    AddrdynamicParameters.Add("@OrderId", result.OrderId);
                    AddrdynamicParameters.Add("@UserId", order.UserId);
                    AddrdynamicParameters.Add("@StateId", order.deliveryAddress.StateId);
                    AddrdynamicParameters.Add("@DistrictId", order.deliveryAddress.DistrictId);
                    AddrdynamicParameters.Add("@CityId", order.deliveryAddress.CityId);
                    AddrdynamicParameters.Add("@Pincode", order.deliveryAddress.Pincode);
                    AddrdynamicParameters.Add("@Address", order.deliveryAddress.Address);

                    var dataresult = await connection.QueryAsync<int>("SaveDeliveryAddress", AddrdynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            return result;
        }

        public async Task<Student> IsStudentExists(string Email)
        {
            Student result = new Student();
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmailId", Email);
                var data = await connection.QueryAsync<Student>("IsStudentExists", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

        public async Task<int> AddProduct(Product product)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@CategoryId", product.CategoryId);
                dynamicParameters.Add("@ProductName", product.ProductName);
                dynamicParameters.Add("@ProductPrice", product.ProductPrice);
                dynamicParameters.Add("@Descriptions", product.Description);
                dynamicParameters.Add("@ProductPath", product.FilePath);
                dynamicParameters.Add("@StockCount", product.StockCount);
                var data = await connection.QueryAsync<int>("SaveProduct", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();

                    if (product.productFeatures.Count > 0)
                    {
                        for (int i = 0; i < product.productFeatures.Count; i++)
                        {
                            DynamicParameters dynamicParametersfeture = new DynamicParameters();
                            dynamicParametersfeture.Add("@FeatureId", product.productFeatures[i].id);
                            dynamicParametersfeture.Add("@FeatureName", product.productFeatures[i].name);
                            dynamicParametersfeture.Add("@ProductId", result);
                            var feturedata = await connection.QueryAsync<int>("AddProductFeature", dynamicParametersfeture, commandType: System.Data.CommandType.StoredProcedure);
                        }
                    }
                }
            }
            return result;
        }


        public async Task<int> SendEmail(Email email)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@To", email.To);
                dynamicParameters.Add("@From", email.From);
                dynamicParameters.Add("@Subject", email.Subject);
                dynamicParameters.Add("@Content", email.Content);
                var data = await connection.QueryAsync<int>("SendEmail", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }
        public async Task<int> UpdateUserProfile(UserProfile userprofile)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", userprofile.Id);
                dynamicParameters.Add("@ProfilePath", userprofile.FilePath);
                try
                {
                    var data = await connection.QueryAsync<int>("UpdateUserProfile", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = data.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                { 
                
                }
                
            }
            return result;
        }


        public async Task<SearchProduct> GetProductListFilter(SearchProduct serachfilter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Product> EmpList = new List<Product>();
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    SearchProduct result = new SearchProduct();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@SearchTerm", serachfilter.SearchFilter);
                    dynamicParameters.Add("@Page", serachfilter.PageNumber);
                    dynamicParameters.Add("@Size", serachfilter.PageSize);
                    //dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var reader = await connection.QueryMultipleAsync("GetAllProductByPagination", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    EmpList = reader.Read<Product>().ToList();
                    result.TotalCount = dynamicParameters.Get<int>("RecordCount");
                    result.product = EmpList;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SearchProduct> GetCartProduct(SearchProduct serachfilter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Product> EmpList = new List<Product>();
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    SearchProduct result = new SearchProduct();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@SearchTerm", serachfilter.SearchFilter);
                    dynamicParameters.Add("@UserId", serachfilter.UserId);
                    dynamicParameters.Add("@Page", serachfilter.PageNumber);
                    dynamicParameters.Add("@Size", serachfilter.PageSize);
                    //dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var reader = await connection.QueryMultipleAsync("GetCartItems", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    EmpList = reader.Read<Product>().ToList();
                    result.TotalCount = dynamicParameters.Get<int>("RecordCount");
                    result.product = EmpList;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<SearchOrder> GetOrderByUserId(SearchOrder serachfilter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Order> EmpList = new List<Order>();
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    SearchOrder result = new SearchOrder();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@SearchTerm", serachfilter.SearchFilter);
                    dynamicParameters.Add("@UserId", serachfilter.UserId);
                    dynamicParameters.Add("@Page", serachfilter.PageNumber);
                    dynamicParameters.Add("@Size", serachfilter.PageSize);
                    //dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var reader = await connection.QueryMultipleAsync("GetOrderByUser", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    EmpList = reader.Read<Order>().ToList();
                    result.TotalCount = dynamicParameters.Get<int>("RecordCount");
                    result.Orders = EmpList;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SearchUser> GetUsersListFilter(SearchUser serachfilter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<User> EmpList = new List<User>();
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    SearchUser result = new SearchUser();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@SearchTerm", serachfilter.SearchFilter);
                    dynamicParameters.Add("@Page", serachfilter.PageNumber);
                    dynamicParameters.Add("@Size", serachfilter.PageSize);
                    //dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    dynamicParameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var reader = await connection.QueryMultipleAsync("GetUsersByPagination", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    EmpList = reader.Read<User>().ToList();
                    result.TotalCount = dynamicParameters.Get<int>("RecordCount");
                    result.Users = EmpList;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Product> GetProductById(int ProductId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    Product EmpList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@ProductId", ProductId);
                    EmpList = (await connection.QueryAsync<Product>("getProductbyId", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserEmail> GetEmailDetails(int MaildId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    UserEmail email;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@MaildId", MaildId);
                    email = (await connection.QueryAsync<UserEmail>("getMaildDetailsById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    return email;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<UserEmail>> GetEmailByUserId(int UserId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<UserEmail> emailList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@UserId", UserId);
                    emailList = (await connection.QueryAsync<UserEmail>("GetEmailList", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                    //emailList = (await connection.QueryAsync<List<UserEmail>>("GetEmailList", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    return emailList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderDetails> GetOrderByOrderId(string OrderId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    OrderDetails Orderdetails;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@OrderId", OrderId);
                    Orderdetails = (await connection.QueryAsync<OrderDetails>("GetOrderDetailsById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                    return Orderdetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductComment(ProductComment comment)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ProductId", comment.ProductId);
                dynamicParameters.Add("@Comments", comment.Comments);
                dynamicParameters.Add("@EmpId", comment.EmpId);

                var data = await connection.QueryAsync<int>("SaveComments", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }


        public async Task<int> AddUserNotyme(int ProductId, string Email, int Id )
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ProductId", ProductId);
                dynamicParameters.Add("@EmailId", Email);
                dynamicParameters.Add("@Id", Id);
                try
                {
                    var data = await connection.QueryAsync<int>("AddSubscriber", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = data.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                { 
                
                }
               
            }
            return result;
        }


        public async Task<int> RemoveUserNotyme(int ProductId, string Email, int Id)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ProductId", ProductId);
                dynamicParameters.Add("@EmailId", Email);
                dynamicParameters.Add("@Id", Id);
                try
                {
                    var data = await connection.QueryAsync<int>("UnSubscriber", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = data.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return result;
        }

        public async Task<int> DeleteEmails(int[] EmailIds)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmailId", EmailIds);
                try
                {
                    var data = await connection.QueryAsync<int>("UnSubscriber", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = data.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return result;
        }

        public async Task<int> setStockCount(int NewStock, int ProductId)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@StockCount", NewStock);
                dynamicParameters.Add("@ProductId", ProductId);
                try
                {
                    var data = await connection.QueryAsync<int>("SetProductStock", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = data.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return result;
        }


        public async Task<List<EmailSubscription>> GetProducSubscriber(int ProductId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<EmailSubscription> EmpList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@ProductId", ProductId);
                    EmpList = (await connection.QueryAsync<EmailSubscription>("GetSubscriber", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductComment>> GetProductComment(int ProductId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<ProductComment> EmpList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@ProductId", ProductId);
                    EmpList = (await connection.QueryAsync<ProductComment>("GetProductComments", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Product>> GetCartItemByUserId(int UserId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<Product> ProductList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@UserId", UserId);
                    ProductList = (await connection.QueryAsync<Product>("GetCartItemsByUserId", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return ProductList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductFeatures>> GetProdutFeatureById(int ProductId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<ProductFeatures> featureList;

                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@ProductId", ProductId);
                    featureList = (await connection.QueryAsync<ProductFeatures>("GetProductFeatureById", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return featureList;
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

        public async Task<List<DropDown>> GetBankList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<DropDown> EmpList;

                    EmpList = (await connection.QueryAsync<DropDown>("GetBankList", null, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderCount>> GetOrderCount(string Role,int UserId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<OrderCount> countList;

                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Role", Role);
                    dynamicParameters.Add("@UserId", UserId);

                    countList = (await connection.QueryAsync<OrderCount>("GetOrderDashboard", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

                    return countList;
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

        public async Task<List<DropDown>> GetCitytByDistrict(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<DropDown> EmpList;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Id);
                    EmpList = (await connection.QueryAsync<DropDown>("GetCityByDistrict", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DropDown>> GetIFSCByBank(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
                {
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    List<DropDown> EmpList;
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Id);
                    EmpList = (await connection.QueryAsync<DropDown>("GetIFSCByBank", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();


                    return EmpList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddToCart(Order order)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(commonRepository.EmpConnection))
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", order.UserId);
                dynamicParameters.Add("@ProductId", order.ProductId);
               
                var data = await connection.QueryAsync<int>("SaveOrdeCart", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                if (data != null)
                {
                    result = data.FirstOrDefault();
                }
            }
            return result;
        }

    }
}
