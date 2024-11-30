using Services.Common;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.SubscriberOperation
{

   

    public class EmailNotification : NotificationAlertObserver
    {
        public int ProductId { get; set; }
        public string EmailId = string.Empty;
        public int UserId;
        private  IStudentRepository _StudentRepository;
        public EmailNotification(int productId, string emailId, int userId)
        {
            ProductId = productId;
            EmailId = emailId;
            UserId = userId;
        }

        public void Update()
        {
            SendEmail();
            _StudentRepository.RemoveUserNotyme(ProductId, EmailId, UserId);
            // Console.WriteLine("Send mail to registered email " + EmailId + "address.....!");
        }

        public void SendEmail()
        {


            _StudentRepository = new StudentRepository();
            Email email = new Email();
            email.To = EmailId;
            email.From = Constant.From;
            email.Subject = Constant.Subject;
            email.Content = "Hi Dear Custemer, Product is available now, please check below link ,"+ Constant.ProductUrl + ProductId;
            _StudentRepository.SendEmail(email);


        }
    }
}
