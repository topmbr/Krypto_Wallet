using Krypto_Wallet.Models;
using Microsoft.EntityFrameworkCore;

namespace Krypto_Wallet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CryptoRecord> CryptoRecords { get; set; }
    }
}
