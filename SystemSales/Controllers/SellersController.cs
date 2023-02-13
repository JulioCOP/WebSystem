using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;
using SystemSales.Services;
using SystemSales.Models.ViewModels;

namespace SystemSales.Controllers
{
    public class SellersController : Controller
    {
        // Declarar uma dependência
        private readonly SellerService _sellerService;

        private readonly ServiceDepartment _serviceDepartment;

        public SellersController(SellerService sellerService, ServiceDepartment serviceDepartment)
        {
            _sellerService = sellerService;
            _serviceDepartment = serviceDepartment;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        
        public IActionResult Create()
        {
            // atualizar o metodo para cadastrar o vendedor

            var departments = _serviceDepartment.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel); // quando a tela de cadastro for acionada irá retornar com o objeto ViewModel
        }

        [HttpPost] //ação de post
        [ValidateAntiForgeryToken]// previnir a aplicação de forer ataques CSR, através do aen
        public IActionResult Create(Seller seller)
        {
            _sellerService.InsertSeller(seller);
            // redireções a inserção para o index, onde será mostrado ao usuário
            return (RedirectToAction(nameof(Index))); //melhora a manutenção do sistema, mantendo o código atual
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index)); // redirecionar para tela inicial de vendedores do CRUD
        }
    }
}
