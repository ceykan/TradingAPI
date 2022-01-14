using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.Data.Entities;

namespace TradingAPI.Data.Repositories
{

    public interface IQuotesRepository
    {
        Task<IEnumerable<Company>> GetAllCompanyLookUpAsync();
        Task<IEnumerable<StockQuotes>> GetCompanysStockQuotesAsync();
        Task<IEnumerable<StockQuotes>> GetCompanyStockQuotesAsync(string symbol);
    }

}
