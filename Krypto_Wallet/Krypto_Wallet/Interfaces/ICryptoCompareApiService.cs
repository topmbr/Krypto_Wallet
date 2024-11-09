using System.Threading.Tasks;

namespace Krypto_Wallet.Interfaces
{
    public interface ICryptoCompareApiService
    {
        Task<string> GetCryptoPriceAsync(string symbol, string currencies = "USD");
    }
}
