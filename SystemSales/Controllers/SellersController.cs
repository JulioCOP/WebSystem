using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;
using SystemSales.Services;
namespace SystemSales.Controllers
{
    public class SellersController : Controller
    {
        // Declarar uma dependência
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //ação de post
        [ValidateAntiForgeryToken]// previnir a aplicação de forer ataques CSR, através do aen
        public IActionResult Create(Seller seller)
        {
            _sellerService.InsertSeller(seller);
            // redireções a inserção para o index, onde será mostrado ao usuário
            return (RedirectToAction(nameof(Index))); //melhora a manutenção do sistema, mantendo o código atual
        }
    }
}
