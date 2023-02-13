using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;
using SystemSales.Services;
using SystemSales.Models.ViewModels;
using SystemSales.Services.Exceptions;

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
            if (id == null) //Verifica se o ID é valido e se foi informado corretamente
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value); //testa se o ID existe no banco de dados
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
        public IActionResult Details(int? id)
        {
            // Mesma lógica do metodo delete
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
            // busca o id do produto e qual objeto o mesmo se refere, para depois retornala-lo na view
        }
        public IActionResult Edit(int? id) //abrir uma nova tela para editar os dados dos vendedores
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
            List<Department> departments = _serviceDepartment.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
