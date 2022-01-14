using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Data.Entities
{
    public class Company
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
    }
}
