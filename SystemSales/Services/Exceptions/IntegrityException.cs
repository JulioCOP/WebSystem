 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSales.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException (string message) : base(message)
        // exceção personalizada de serviço para erros integridade referencial
        {

        }
         
    }
}
