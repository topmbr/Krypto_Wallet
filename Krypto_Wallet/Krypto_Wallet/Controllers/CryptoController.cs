using Microsoft.AspNetCore.Mvc;

namespace Krypto_Wallet.Controllers
{
    public class CryptoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
