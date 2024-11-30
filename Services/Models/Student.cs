using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Student
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public bool IsAuthenticate { get; set; }
       // public bool IsActive { get; set; }
        //public string Token { get; set; }
        public string Language { get; set; }
        public bool Gender { get; set; }
        public DateTime DOB { get; set; }
        public bool IsAuthenticate { get; set; }
        public string RoleName { get; set; }
        public byte[] ProfileFile { get; set; }
        public string ProfilePath { get; set; }
        //public int Requested { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double ProductPrice { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public byte[] ProductFile { get; set; }
        public string ProductPath { get; set; }
        public int StockCount { get; set; }

        public List<ProductFeatures> productFeatures { get; set; }
        public string ProductFeature { get; set; }

        public int NotifyCount { get; set; }
        public int SubscribedUserId { get; set; }
    }

    public class ProductFeatures
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    public class SearchProduct
    {
        public int? UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchFilter { get; set; }
        public int TotalCount { get; set; }
        public List<Product> product { get; set; }
    }

    public class SearchUser
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchFilter { get; set; }
        public int TotalCount { get; set; }
        public List<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string StatusName { get; set; }
        public int Requested { get; set; }
        public int StatusId { get; set; }

        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public byte[] ProfileFile { get; set; }
        public string ProfilePath { get; set; }
    }

    public class ProductComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Comments { get; set; }
        public int EmpId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }

    }

    public class UserProfile
    {
        public int Id { get; set; }

        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        //public byte[] ProductFile { get; set; }
        //public string ProductPath { get; set; }
    }

    public class Payment
    {
        public int BankId { get; set; }
        public string IFSCCode { get; set; }
        public string cardNumber { get; set;}
        public string cardHolder { get; set;}
        public int expiryMonth { get; set;}
        public int expiryYear { get; set;}
        public int cvv { get; set;}
        public float Amount { get; set; }
    }

    public class PaymentSuccess
    {
        public bool IsSuccess { get; set; }
    }

    public class Order
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string OrderId { get; set; }
        public bool IsSuccess { get; set; }
        public DeliveryAddress deliveryAddress { get; set; }
        public byte[] ProductFile { get; set; }
        public string ProductPath { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
    }

    public class DeliveryAddress
    { 
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }

    }

    public class OrderDetails
    {
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public byte[] ProductFile { get; set; }
        public string ProductPath { get; set; }
        public string OrderId { get; set; }

    }

    public class Search
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchFilter { get; set; }
        public int TotalCount { get; set; }
        public List<User> Users { get; set; }
    }

    public class SearchOrder : Search
    {

        public int? UserId { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class OrderCount
    {
        public int OrderCounts { get; set; }
        public string OrderDates { get; set; }
    }

    public class EmailSubscription
    {
       public int ProductId { get; set; }
       public int UserId { get; set; }
       public string EmailId { get; set; }
    }

    public class Email
    { 
    public string To { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    }

    public class UserEmail
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string EmaiFrom { get; set; }
        public string Content { get; set; }
    }

}
