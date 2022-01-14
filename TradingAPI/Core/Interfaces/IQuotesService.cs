using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.Business.Entities;

namespace TradingAPI.Core.Interfaces
{
    public interface IQuotesService
    {
        Task<IEnumerable<QuotesResponseModel>> GetQuotes();
        Task<IEnumerable<QuotesResponseModel>> GetQuoteBySymbol(string symbol);
        Task<IEnumerable<CompanyResponseModel>> GetCompanies();
        Task<IEnumerable<CompanyResponseModel>> GetCompanyBySymbol(string symbol);
    }
}
