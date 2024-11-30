using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ResponseFormat
{
    [Serializable]
    public class ResultEntity<T> where T : new()
    {
        public ResultEntity()
        {
            Entity = new T();
        }
        public T Entity { get; set; }
        //public StatusTypeEnum Status { get; set; }
        public string Message { get; set; }
        public string CachingStatus { set; get; }
    }
}
