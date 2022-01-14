using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TradingAPI.Core;
using TradingAPI.Core.Interfaces;
using TradingAPI.Data.Entities;

namespace TradingAPI.Data.Repositories
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly IVentorService _ventorService;
        private readonly IConfiguration _configuration;
        public readonly ILogger<QuotesRepository> _logger;
        public readonly string currentVentor;
        public QuotesRepository(IConfiguration configuration, ILogger<QuotesRepository> logger)
        {
            this._configuration = configuration;
            this.currentVentor = _configuration["CurrentVentor"];
            this._logger = logger;
            _ventorService = VentorFactory.GenerateVentor(currentVentor);
        }

        public async Task<IEnumerable<Company>> GetAllCompanyLookUpAsync()
        {
            try
            {
                var list = await _ventorService.MapVentorAllCompanyInfoAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error on {0} - {1} - message: {2}"), MethodBase.GetCurrentMethod().Name, this.currentVentor, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<StockQuotes>> GetCompanysStockQuotesAsync()
        {
            try
            {
                var quotes = await _ventorService.MapVentorAllQuotesAsync();
                return quotes;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error on {0} - {1} - message: {2}"), MethodBase.GetCurrentMethod().Name, this.currentVentor, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<StockQuotes>> GetCompanyStockQuotesAsync(string symbol)
        {
            try
            {
                var quote = await _ventorService.MapVentorQuoteInfoAsync(symbol);
                return quote;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error on {0} - {1} - message: {2}"), MethodBase.GetCurrentMethod().Name, this.currentVentor, ex.Message);
                throw;
            }
        }
    }
}

