using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;

namespace SystemSales.Services
{
    public class SellerService
    {
        // Precisa de uma dependência ára SystemSalesContext

        private readonly SystemSalesContext _context;

        public SellerService(SystemSalesContext context)
        {
            _context = context;
        }

        // Operação FindAll para retornar todos os vendedores da lista do banco de dados

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // retorna do banco de dados todos os vendedores
        }

        // inserir um novo vendedor no banco de dados
        public void InsertSeller(Seller obj) //objeto Seller já está instanciado com departamento na classe SeriveDepartment
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
