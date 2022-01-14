using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.Business.Entities;
using TradingAPI.Core.Interfaces;
using TradingAPI.Data.Repositories;

namespace TradingAPI.Business
{
    public class QuotesService : IQuotesService
    {
        public IQuotesRepository _repo;
        public QuotesService(IQuotesRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CompanyResponseModel>> GetCompanies()
        {
            var data = await _repo.GetAllCompanyLookUpAsync();
            List<CompanyResponseModel> responseModel = new();
            if (data != null)
            {
                responseModel = (from d in data
                                 select new CompanyResponseModel
                                 {
                                     Name = d.Name,
                                     Symbol = d.Symbol,
                                     Exchange = d.Exchange
                                 }).ToList();
            }

            return responseModel;
        }

        public async Task<IEnumerable<CompanyResponseModel>> GetCompanyBySymbol(string symbol)
        {
            var data = await this.GetCompanies();
            List<CompanyResponseModel> responseModel = new();
            if (data != null)
            {
                var dataSingle = data.Where(i => i.Symbol == symbol).ToList();
                responseModel = (from d in dataSingle
                                 select new CompanyResponseModel() { Exchange = d?.Exchange, Name = d?.Name, Symbol = d?.Symbol }).ToList();
            }
            return responseModel;
        }

        public async Task<IEnumerable<QuotesResponseModel>> GetQuoteBySymbol(string symbol)
        {
            var data = await _repo.GetCompanyStockQuotesAsync(symbol);
            List<QuotesResponseModel> responseModel = new();
            if (data != null)
            {
                responseModel = (from l in data
                                 where l.Symbol == symbol
                                 select new QuotesResponseModel()
                                 {
                                     Name = l.Name,
                                     Symbol = l.Symbol,
                                     Exchange = l.Exchange,
                                     LastPrice = l.LastPrice,
                                     Change = l.Change,
                                     ChangePercent = l.ChangePercent,
                                     MSDate = l.MSDate,
                                     MarketCap = l.MarketCap,
                                     Volume = l.Volume,
                                     ChangeYTD = l.ChangeYTD,
                                     ChangePercentYTD = l.ChangePercentYTD,
                                     High = l.High,
                                     Low = l.Low,
                                     Open = l.Open
                                 }).ToList();
            }

            return responseModel;
        }

        public async Task<IEnumerable<QuotesResponseModel>> GetQuotes()
        {
            var data = await _repo.GetCompanysStockQuotesAsync();
            List<QuotesResponseModel> responseModel = new();
            if (data != null)
            {
                responseModel = (from d in data
                                 select new QuotesResponseModel
                                 {
                                     Name = d.Name,
                                     Symbol = d.Symbol,
                                     Exchange = d.Exchange,
                                     LastPrice = d.LastPrice,
                                     Change = d.Change,
                                     ChangePercent = d.ChangePercent,
                                     MSDate = d.MSDate,
                                     MarketCap = d.MarketCap,
                                     Volume = d.Volume,
                                     ChangeYTD = d.ChangeYTD,
                                     ChangePercentYTD = d.ChangePercentYTD,
                                     High = d.High,
                                     Low = d.Low,
                                     Open = d.Open
                                 }).ToList();
            }
            return responseModel;
        }
    }
}
