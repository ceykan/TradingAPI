using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.Core;
using TradingAPI.Core.Interfaces;
using TradingAPI.Data.Entities;

namespace TradingAPI.Data.Modules.Reuters
{
    public class ReutersService : IVentorService
    {

        public async Task<IEnumerable<StockQuotes>> MapVentorAllQuotesAsync()
        {
            var ventorData = await CommonHelper.ReadFromUrl("https://mocki.io/v1/891b5a59-799c-4d86-845a-f199dd16cea6");
            List<StockQuotes> data = new();
            data =
           (from l in ventorData
            select new StockQuotes
            {
                Name = l.Name,
                Symbol = l.Symbol,
                Exchange = l.Exchange,
                LastPrice = l.LastPrice,
                Change = l.Change,
                ChangePercent = l.ChangePercent,
                MSDate = l.MSDate,
                MarketCap = Convert.ToInt64(l.MarketCap),
                Volume = Convert.ToInt32(l.Volume),
                ChangeYTD = l.ChangeYTD,
                ChangePercentYTD = l.ChangePercentYTD,
                High = l.High,
                Low = l.Low,
                Open = l.Open
            }).ToList();
            return data;
        }

        public async Task<IEnumerable<StockQuotes>> MapVentorQuoteInfoAsync(string symbol)
        {
            var ventorData = await CommonHelper.ReadFromUrl("https://mocki.io/v1/891b5a59-799c-4d86-845a-f199dd16cea6");
            var data =
                (from l in ventorData
                 where l.Symbol == symbol
                 select new StockQuotes
                 {
                     Name = l.Name,
                     Symbol = l.Symbol,
                     Exchange = l.Exchange,
                     LastPrice = l.LastPrice,
                     Change = l.Change,
                     ChangePercent = l.ChangePercent,
                     MSDate = l.MSDate,
                     MarketCap = Convert.ToInt64(l.MarketCap),
                     Volume = Convert.ToInt32(l.Volume),
                     ChangeYTD = l.ChangeYTD,
                     ChangePercentYTD = l.ChangePercentYTD,
                     High = l.High,
                     Low = l.Low,
                     Open = l.Open
                 }).ToList();
            return data;
        }

        public async Task<List<Company>> MapVentorCompanyInfoAsync(string symbol)
        {
            var companyData = await this.MapVentorAllCompanyInfoAsync();
            var data = companyData.Where(i => i.Symbol == symbol).ToList();
            return data;

        }

        public async Task<List<Company>> MapVentorAllCompanyInfoAsync()
        {
            var companyData = await CommonHelper.ReadFromUrl("https://mocki.io/v1/891b5a59-799c-4d86-845a-f199dd16cea6"); // URL may be different for a single company
            var data =
                (from l in companyData
                 select new Company
                 {
                     Name = l.Name,
                     Symbol = l.Symbol,
                     Exchange = l.Exchange
                 }).ToList();
            return data;
        }
    }
}
