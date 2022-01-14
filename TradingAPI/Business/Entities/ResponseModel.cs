using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TradingAPI.Business.Entities
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
