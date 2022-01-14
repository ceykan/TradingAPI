using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Business.Entities
{
    public class CompanyResponseModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
    }
}
