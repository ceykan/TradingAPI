using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TradingAPI.Core;
using TradingAPI.Core.Interfaces;
using TradingAPI.Data.Entities;

namespace TradingAPI.Data.Modules.Bloomberg
{
    public class BloombergService : IVentorService
    {
        public async Task<List<Company>> MapVentorCompanyInfoAsync(string symbol)
        {
            var companyData = await this.MapVentorAllCompanyInfoAsync();
            var data = companyData.Where(i => i.Symbol == symbol).ToList();
            return data;
        }

        public async Task<List<Company>> MapVentorAllCompanyInfoAsync()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TradingData.csv");
            var companyData = await CommonHelper.ReadFromFile(filePath, true);
            var data = (from l in companyData
                        select new Company
                        {
                            Name = l[0],
                            Symbol = l[1],
                            Exchange = l[2]
                        }).ToList();
            return data;
        }

        public async Task<IEnumerable<StockQuotes>> MapVentorAllQuotesAsync()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TradingData.csv");

            var vendorData = await CommonHelper.ReadFromFile(filePath, true);
            var data =
                (from l in vendorData
                 select new StockQuotes
                 {
                     Name = l[0],
                     Symbol = l[1],
                     Exchange = l[2],
                     LastPrice = Double.Parse(l[3], CultureInfo.InvariantCulture),
                     Change = Double.Parse(l[4], CultureInfo.InvariantCulture),
                     ChangePercent = Double.Parse(l[5], CultureInfo.InvariantCulture),
                     // TimeStamp = 
                     MSDate = Double.Parse(l[7], CultureInfo.InvariantCulture),
                     MarketCap = Convert.ToInt64(l[8], CultureInfo.InvariantCulture),
                     Volume = Convert.ToInt32(l[9], CultureInfo.InvariantCulture),
                     ChangeYTD = Double.Parse(l[10], CultureInfo.InvariantCulture),
                     ChangePercentYTD = Double.Parse(l[11], CultureInfo.InvariantCulture),
                     High = Double.Parse(l[12], CultureInfo.InvariantCulture),
                     Low = Double.Parse(l[13], CultureInfo.InvariantCulture),
                     Open = Double.Parse(l[14], CultureInfo.InvariantCulture)
                 }).ToList();
            return data;
        }

        public async Task<IEnumerable<StockQuotes>> MapVentorQuoteInfoAsync(string symbol)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "TradingData.csv");

            var vendorData = await CommonHelper.ReadFromFile(filePath, true);
            var data =
                (from l in vendorData
                 where l[1] == symbol
                 select new StockQuotes
                 {
                     Name = l[0],
                     Symbol = l[1],
                     Exchange = l[2],
                     LastPrice = Double.Parse(l[3], CultureInfo.InvariantCulture),
                     Change = Double.Parse(l[4], CultureInfo.InvariantCulture),
                     ChangePercent = Double.Parse(l[5], CultureInfo.InvariantCulture),
                     MSDate = Double.Parse(l[7], CultureInfo.InvariantCulture),
                     MarketCap = Convert.ToInt64(l[8], CultureInfo.InvariantCulture),
                     Volume = Convert.ToInt32(l[9], CultureInfo.InvariantCulture),
                     ChangeYTD = Double.Parse(l[10], CultureInfo.InvariantCulture),
                     ChangePercentYTD = Double.Parse(l[11], CultureInfo.InvariantCulture),
                     High = Double.Parse(l[12], CultureInfo.InvariantCulture),
                     Low = Double.Parse(l[13], CultureInfo.InvariantCulture),
                     Open = Double.Parse(l[14], CultureInfo.InvariantCulture)
                 }).ToList();
            return data;
        }

    }
}
