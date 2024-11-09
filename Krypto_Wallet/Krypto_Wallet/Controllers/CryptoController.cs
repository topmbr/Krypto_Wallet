using Krypto_Wallet.Interfaces;
using Krypto_Wallet.Models;
using Krypto_Wallet.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Krypto_Wallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoPriceRepository _cryptoPriceRepository;
        private readonly ICryptoCompareApiService _cryptoCompareApiService;

        public CryptoController(ICryptoPriceRepository cryptoPriceRepository, ICryptoCompareApiService cryptoCompareApiService)
        {
            _cryptoPriceRepository = cryptoPriceRepository;
            _cryptoCompareApiService = cryptoCompareApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _cryptoPriceRepository.GetAllAsync();
            return Ok(records);
        }
        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetCryptoPrice(string symbol)
        {
            try
            {
                // Получаем данные с сайта через сервис
                var jsonResult = await _cryptoCompareApiService.GetCryptoPriceAsync(symbol);
                var prices = JsonSerializer.Deserialize<Dictionary<string, decimal>>(jsonResult);

                if (prices != null && prices.ContainsKey("USD"))
                {
                    var record = new CryptoRecord
                    {
                        Symbol = symbol.ToUpper(),
                        Price = prices["USD"],
                        RetrievedAt = DateTime.UtcNow
                    };

                    var existingRecord = await _cryptoPriceRepository.GetBySymbolAsync(symbol);
                    if (existingRecord != null)
                    {
                        existingRecord.Price = record.Price;
                        existingRecord.RetrievedAt = record.RetrievedAt;
                        await _cryptoPriceRepository.UpdateAsync(existingRecord);
                    }
                    else
                    {
                        await _cryptoPriceRepository.AddAsync(record);
                    }

                    return Ok(record);
                }

                return NotFound(new { Message = "Price not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred", Details = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CryptoRecord record)
        {
            if (id != record.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var updatedRecord = await _cryptoPriceRepository.UpdateAsync(record);
            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _cryptoPriceRepository.DeleteAsync(id);
            if (!success)
                return NotFound(new { Message = "Record not found" });

            return Ok(new { Message = "Record deleted" });
        }
    }
}
