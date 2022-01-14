using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TradingAPI.Business.Entities;
using TradingAPI.Core.Caching.Provider;
using TradingAPI.Core.Interfaces;

namespace TradingAPI.Controllers
{
    [ApiController]
    [Route("quote")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuotesService _service;
        private readonly ICacheProvider _cacheProvider;
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(IQuotesService service, ICacheProvider cacheProvider, ILogger<QuoteController> logger)
        {
            this._service = service;
            this._logger = logger;
            this._cacheProvider = cacheProvider;
        }

        [HttpGet]
        [Route("getAllQuotes")]
        public async Task<ActionResult<IEnumerable<QuotesResponseModel>>> GetAllQuotes()
        {
            ResponseModel<IEnumerable<QuotesResponseModel>> response = new();
            var cacheKey = "quotesList";
            try
            {
                var cacheExists = await _cacheProvider.ExistsAsync(cacheKey);
                if (!cacheExists)
                {

                    IEnumerable<QuotesResponseModel> quotes = await _service.GetQuotes();
                    await _cacheProvider.SetAsync(cacheKey, quotes, TimeSpan.FromSeconds(75));
                    response.Data = quotes;
                }
                else
                {
                    response.Data = await _cacheProvider.GetAsync<IEnumerable<QuotesResponseModel>>(cacheKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.StatusCode = HttpStatusCode.NotFound;
                response.StatusMessage = "Error. Contact with the provider";
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("getQuoteBySymbol")]
        public async Task<ActionResult<IEnumerable<QuotesResponseModel>>> GetQuote([FromHeader] string symbol)
        {
            ResponseModel<IEnumerable<QuotesResponseModel>> response = new();
            if (string.IsNullOrEmpty(symbol))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.StatusMessage = "Parameter needed.";
                return BadRequest(response);
            }
            var cacheKey = string.Format("{0}-quotesList", symbol);
            try
            {
                var cacheExists = await _cacheProvider.ExistsAsync(cacheKey);
                if (!cacheExists)
                {
                    IEnumerable<QuotesResponseModel> quote = await _service.GetQuoteBySymbol(symbol);
                    await _cacheProvider.SetAsync(cacheKey, quote, TimeSpan.FromSeconds(20));
                    response.Data = quote;
                }
                else
                {
                    response.Data = await _cacheProvider.GetAsync<IEnumerable<QuotesResponseModel>>(cacheKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.StatusCode = HttpStatusCode.NotFound;
                response.StatusMessage = "Error. Contact with the provider";
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllCompanies")]
        public async Task<ActionResult<IEnumerable<CompanyResponseModel>>> GetAllCompanies()
        {
            var cacheKey = "getAllCompanies";
            ResponseModel<IEnumerable<CompanyResponseModel>> response = new();
            try
            {
                var cacheExists = await _cacheProvider.ExistsAsync(cacheKey);
                if (!cacheExists)
                {
                    IEnumerable<CompanyResponseModel> companies = await _service.GetCompanies();
                    await _cacheProvider.SetAsync(cacheKey, companies, TimeSpan.FromSeconds(20));
                    response.Data = companies;
                }
                else
                {
                    response.Data = await _cacheProvider.GetAsync<IEnumerable<CompanyResponseModel>>(cacheKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.StatusCode = HttpStatusCode.NotFound;
                response.StatusMessage = "Error. Contact with the provider";
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("getCompany")]
        public async Task<ActionResult<IEnumerable<CompanyResponseModel>>> GetCompany([FromHeader] string symbol)
        {
            ResponseModel<IEnumerable<CompanyResponseModel>> response = new();
            if (string.IsNullOrEmpty(symbol))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.StatusMessage = "Parameter needed.";
                return BadRequest(response);
            }
            var cacheKey = string.Format("{0}-getCompany", symbol);
            try
            {
                var cacheExists = await _cacheProvider.ExistsAsync(cacheKey);
                if (!cacheExists)
                {
                    IEnumerable<CompanyResponseModel> company = await _service.GetCompanyBySymbol(symbol);
                    await _cacheProvider.SetAsync(cacheKey, company, TimeSpan.FromSeconds(20));
                    response.Data = company;
                }
                else
                {
                    response.Data = await _cacheProvider.GetAsync<IEnumerable<CompanyResponseModel>>(cacheKey);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.StatusCode = HttpStatusCode.NotFound;
                response.StatusMessage = "Error. Contact with the provider";
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
