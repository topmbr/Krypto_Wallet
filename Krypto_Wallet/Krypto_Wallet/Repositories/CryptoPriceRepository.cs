using Krypto_Wallet.Data;
using Krypto_Wallet.Interfaces;
using Krypto_Wallet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Krypto_Wallet.Repositories
{
    public class CryptoPriceRepository : ICryptoPriceRepository
    {
        private readonly ApplicationDbContext _context;

        public CryptoPriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CryptoRecord>> GetAllAsync()
        {
            return await _context.CryptoRecords.ToListAsync();
        }

        public async Task<CryptoRecord> GetBySymbolAsync(string symbol)
        {
            return await _context.CryptoRecords.FirstOrDefaultAsync(r => r.Symbol == symbol.ToUpper());
        }

        public async Task<CryptoRecord> AddAsync(CryptoRecord record)
        {
            _context.CryptoRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<CryptoRecord> UpdateAsync(CryptoRecord record)
        {
            _context.CryptoRecords.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.CryptoRecords.FindAsync(id);
            if (record == null) return false;

            _context.CryptoRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
