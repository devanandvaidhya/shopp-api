using Services.Models;
using Services.Repository.SubscriberOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IStudentRepository
    {
        public Task<int> AddStudent(Student student);
        public Task<int> AddToCart(Order order);
        public Task<Student> GetStudentById(int Id);
        public Task<List<Student>> GetStudent();
        public Task<List<Product>> GetProductList();
        public Task<List<Category>> GetCategories();
        public Task<int> DeleteStudentById(int Id);
        public Task<int> UpdateStudent(Student student);
        public Task<int> SendRequest(int  Id);
        public Task<int> UpdateStatus(int Id,int StatusId, string Comment);
        public Task<Student> UserAuthentication(Login login);
        public Task<PaymentSuccess> PaymentProcess(Payment login);
        public Task<Order> OrderProcess(Order order);
        public Task<Student> IsStudentExists(string Email);
        public Task<int> AddProduct(Product employee);
        public Task<int> SendEmail(Email email);
        public Task<SearchProduct> GetProductListFilter(SearchProduct searrchfilter);
        public Task<SearchProduct> GetCartProduct(SearchProduct searrchfilter);
        public Task<SearchOrder> GetOrderByUserId(SearchOrder searrchfilter);
        public Task<SearchUser> GetUsersListFilter(SearchUser searrchfilter);

        public Task<Product> GetProductById(int ProductId);
        public Task<UserEmail> GetEmailDetails(int MailId);
        public Task<List<UserEmail>> GetEmailByUserId(int UserId);
        public Task<int> DeleteEmails(int[] MailIds);
        public Task<OrderDetails> GetOrderByOrderId(string ProductId);
        public Task<int> SaveProductComment(ProductComment student);
        public Task<int> AddUserNotyme(int ProductId, string EmailId, int Id);
        public Task<int> RemoveUserNotyme(int ProductId, string EmailId, int Id);
        public Task<int> setStockCount(int NewStock,  int ProdcutId);
        public Task<List<EmailSubscription>> GetProducSubscriber(int ProductId);
        public Task<List<ProductComment>> GetProductComment(int ProductId);
        public Task<List<Product>> GetCartItemByUserId(int UserId);
        public Task<List<ProductFeatures>> GetProdutFeatureById(int ProductId);
        public Task<int> UpdateUserProfile(UserProfile userProfile);
        public Task<List<DropDown>> GetStateList();
        public Task<List<DropDown>> GetBankList();
        public Task<List<OrderCount>> GetOrderCount(string Role, int UserId);
        public Task<List<DropDown>> GetDistByState(int Id);
        public Task<List<DropDown>> GetCitytByDistrict(int Id);
        public Task<List<DropDown>> GetIFSCByBank(int Id);

    }
}
