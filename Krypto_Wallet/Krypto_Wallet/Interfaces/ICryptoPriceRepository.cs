using Krypto_Wallet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Krypto_Wallet.Interfaces
{
    public interface ICryptoPriceRepository
    {
        Task<IEnumerable<CryptoRecord>> GetAllAsync();
        Task<CryptoRecord> GetBySymbolAsync(string symbol);
        Task<CryptoRecord> AddAsync(CryptoRecord record);
        Task<CryptoRecord> UpdateAsync(CryptoRecord record);
        Task<bool> DeleteAsync(int id);
    }
}
