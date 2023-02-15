using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;

namespace SystemSales.Services
{
    public class SalesRecordService
    {
        private readonly SystemSalesContext _context;

        public SalesRecordService(SystemSalesContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //lógica para encontrar as vendas dentro do intervalo de vendas informado
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                // restrição de data minima
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x=> x.Seller.Department)
                .OrderByDescending(x=> x.Date)
                .ToListAsync();

        }
    }
}
