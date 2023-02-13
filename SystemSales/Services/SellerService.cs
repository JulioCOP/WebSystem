using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;
using Microsoft.EntityFrameworkCore;
using SystemSales.Services.Exceptions;

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
        public void InsertSeller(Seller obj) //objeto Seller já está instanciado com departamento na classe ServiceDepartment
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); //remove do db set
            _context.SaveChanges(); // operação para o entitie framework confirmar remoção no banco de dados
        }
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) // testando no banco de dados se há algum vendedor x, cujo o ID seja igual  ID do objeto 
            {
                throw new NotFoundException("The ID of this product could not be found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
