using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;

namespace SystemSales.Services
{
    public class ServiceDepartment
    {
        private readonly SystemSalesContext _context;
        public ServiceDepartment(SystemSalesContext context)
        {
            _context = context;
        }
        // Método para retornar todos os departamentos
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
