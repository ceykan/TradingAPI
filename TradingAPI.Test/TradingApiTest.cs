using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradingAPI.Business;
using TradingAPI.Core.Interfaces;
using TradingAPI.Data.Repositories;
using Xunit;

namespace TradingAPI.Test
{
    public class TradingApiTest
    {
        private readonly IQuotesRepository _mockRepo;
        private readonly IQuotesService _mockService;
        private readonly ILogger<QuotesRepository> _mockLogger;
        private IConfiguration _configuration;
        public TradingApiTest()
        {
              _configuration  = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            _mockRepo = new QuotesRepository(_configuration, _mockLogger);
            _mockService = new QuotesService(_mockRepo);
        }
        [Fact]
        public async Task Test_GetAllQuotes()
        {
            var quotes = await _mockService.GetCompanies();
            var res = quotes.ToList();
            Assert.True(res.Count>0);
        }
        [Fact]
        public async Task Test_GetSingleQuote()
        {
            string company = "NXST";
            var res = await _mockService.GetQuoteBySymbol(company);
            Assert.Equal(res.FirstOrDefault().Symbol,company);
        }

        public async Task Test_GetSingleCompany()
        {
            string company = "NXST";
            var res = await _mockService.GetCompanyBySymbol(company);
            Assert.Equal(res.FirstOrDefault().Symbol, company);
        }

        public async Task Test_GetAllCompanies()
        {
            var companies = await _mockService.GetCompanies();
            var res = companies.ToList();
            Assert.True(res.Count > 0);
        }
    }
}
