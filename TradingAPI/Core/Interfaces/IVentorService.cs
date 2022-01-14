using System.Collections.Generic;
using System.Threading.Tasks;
using TradingAPI.Data.Entities;

namespace TradingAPI.Core.Interfaces
{
    public interface IVentorService
    {
        Task<IEnumerable<StockQuotes>> MapVentorAllQuotesAsync();
        Task<IEnumerable<StockQuotes>> MapVentorQuoteInfoAsync(string symbol);
        Task<List<Company>> MapVentorCompanyInfoAsync(string symbol);
        Task<List<Company>> MapVentorAllCompanyInfoAsync();
    }
}
