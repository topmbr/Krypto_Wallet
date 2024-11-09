using Krypto_Wallet.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Krypto_Wallet.Services
{
    public class CryptoCompareApiService : ICryptoCompareApiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public CryptoCompareApiService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://min-api.cryptocompare.com/")
            };
        }

        public async Task<string> GetCryptoPriceAsync(string symbol, string currencies = "USD")
        {
            var requestUri = $"data/price?fsym={symbol.ToUpper()}&tsyms={currencies}&api_key={_apiKey}";
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
