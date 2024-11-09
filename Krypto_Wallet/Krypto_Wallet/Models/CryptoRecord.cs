namespace Krypto_Wallet.Models
{
    public class CryptoRecord
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}
