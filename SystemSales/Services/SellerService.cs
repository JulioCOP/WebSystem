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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); // retorna do banco de dados todos os vendedores
        }

        // inserir um novo vendedor no banco de dados
        public async Task InsertSellerAsync(Seller obj) //objeto Seller já está instanciado com departamento na classe ServiceDepartment
        {
            _context.Add(obj);
            await _context.SaveChangesAsync(); // acessa o banco
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj); //remove do db set
                await _context.SaveChangesAsync(); // operação para o entitie framework confirmar remoção no banco de dados
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("You must delete all sales from this seller in so that he or she is exclued from the system");
            }
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) // testando no banco de dados se há algum vendedor x, cujo o ID seja igual  ID do objeto 
            {
                throw new NotFoundException("The ID of this product could not be found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}

