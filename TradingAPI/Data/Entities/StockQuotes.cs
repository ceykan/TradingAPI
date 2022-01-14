using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAPI.Data.Entities
{
    public class StockQuotes : Company
    {

        public Company GetCompanyLookUpList()
        {
            Company lookUpList = new Company
            {
                Name = this.Name,
                Symbol = this.Symbol,
                Exchange = this.Exchange
            };
            return lookUpList;
        }
        public double LastPrice { get; init; }
        public double Change { get; init; }
        public double ChangePercent { get; init; }
        public DateTime TimeStamp { get; init; }
        public double MSDate { get; init; }
        public Int64 MarketCap { get; init; }
        public int Volume { get; init; }
        public double ChangeYTD { get; init; }
        public double ChangePercentYTD { get; init; }
        public double High { get; init; }
        public double Low { get; init; }
        public double Open { get; init; }
    }
}
