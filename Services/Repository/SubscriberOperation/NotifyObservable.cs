using Newtonsoft.Json;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.SubscriberOperation
{
    public class NotifyObservable : IStockObserable
    {
        public int StockCount = 0;
        private List<NotificationAlertObserver> lstObserver = new List<NotificationAlertObserver>();
        private readonly IStudentRepository _StudentRepository;
        public NotifyObservable(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        public void Add(NotificationAlertObserver observer)
        {
            //lstObserver.Add(observer);
            string jsonstr = JsonConvert.SerializeObject(observer);
            var NotifictionObj = JsonConvert.DeserializeObject<EmailNotification>(jsonstr);
            _StudentRepository.AddUserNotyme(NotifictionObj.ProductId, NotifictionObj.EmailId, NotifictionObj.UserId);
        }

        public int GetStockCount()
        {
            return StockCount;
        }

        public void NotifySubscriber()
        {
            foreach (NotificationAlertObserver observer in lstObserver)
            {
                observer.Update();
            }
        }

        public void Remove(NotificationAlertObserver observer)
        {
            //lstObserver.Remove(observer);
            string jsonstr = JsonConvert.SerializeObject(observer);
            var NotifictionObj = JsonConvert.DeserializeObject<EmailNotification>(jsonstr);
            _StudentRepository.RemoveUserNotyme(NotifictionObj.ProductId, NotifictionObj.EmailId, NotifictionObj.UserId);
        }

        public async void setStockCount(int NewStockCount, int ProductId)
        {
            List<EmailNotification> lstNotification = new List<EmailNotification>();
            _StudentRepository.setStockCount(NewStockCount, ProductId);
            List<EmailSubscription> listofObserver = new List<EmailSubscription>();
            listofObserver = await _StudentRepository.GetProducSubscriber(ProductId);
            string jsonstr = JsonConvert.SerializeObject(listofObserver);
            lstNotification = JsonConvert.DeserializeObject<List<EmailNotification>>(jsonstr);

            lstObserver.AddRange(lstNotification);
            if (StockCount == 0)
            {
                NotifySubscriber();
            }
            //StockCount+= NewStockCount;
            StockCount = StockCount + NewStockCount;
        }
    }
}
