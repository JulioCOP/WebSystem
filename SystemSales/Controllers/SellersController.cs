using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;
using SystemSales.Services;
using SystemSales.Models.ViewModels;
using SystemSales.Services.Exceptions;
using System.Diagnostics;

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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            // atualizar o metodo para cadastrar o vendedor

            var departments = await _serviceDepartment.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel); // quando a tela de cadastro for acionada irá retornar com o objeto ViewModel
        }

        [HttpPost] //ação de post
        [ValidateAntiForgeryToken]// previnir a aplicação de forer ataques CSR, através do aen
        public async Task<IActionResult> Create(Seller seller)
        {
            // condição que não permite que a edição seja realizada sem que os campos sejam informads
            if (!ModelState.IsValid)
            {
                var departments = await _serviceDepartment.FindAllAsync(); // carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertSellerAsync(seller);
            // redireções a inserção para o index, onde será mostrado ao usuário
            return (RedirectToAction(nameof(Index))); //melhora a manutenção do sistema, mantendo o código atual
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) //Verifica se o ID é valido e se foi informado corretamente
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID NOT INFORMED" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value); //testa se o ID existe no banco de dados
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID NOT FOUND" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index)); // redirecionar para tela inicial de vendedores do CRUD
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            // Mesma lógica do metodo delete
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID NOT INFORMED" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID NOT FOUND" });
            }
            return View(obj);
            // busca o id do produto e qual objeto o mesmo se refere, para depois retornala-lo na view
        }
        public async Task<IActionResult> Edit(int? id) //abrir uma nova tela para editar os dados dos vendedores
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID NOT INFORMED" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID NOT FOUND" });
            }
            List<Department> departments = await _serviceDepartment.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _serviceDepartment.FindAllAsync(); // carrega os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ERROR!\nID DIFFERENT" });

            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public IActionResult Error(string message)
        {
            // enviar uma requisição para mensagem de erro
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                // pega o id interno da requisição
            };
            return View(viewModel);
        }
    }
}
