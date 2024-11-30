using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.SubscriberOperation
{
    public interface IStockObserable
    {
        public void Add(NotificationAlertObserver observer);
        public void Remove(NotificationAlertObserver observer);
        public void NotifySubscriber();
        public void setStockCount(int StockCount,int ProductId);
        public int GetStockCount();
    }
}
