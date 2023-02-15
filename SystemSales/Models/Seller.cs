using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SystemSales.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "YOU MUST ENTER THE {0}")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}Name size showld be between {2} and {1}")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "YOU MUST ENTER THE  {0}")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "YOU MUST ENTER THE {0}")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "YOU MUST ENTER THE {0}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(100.0, 20000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

        // Criação dos departamentos cada vendedor possui apenas um unico departamento

        public Department Department { get; set; }

        // Associaçã de 1 vendedor, possui pode possior varias vendas

        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Operação para adicionar uma venda na lista de venda

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        // Retorno sobre o total de vendas em um determinado periodo

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);

            // Retorna uma lista de vendas, através da operação where, que o mesmo irá filtrar do objeto sr
            // tal que o objeto sr esteja entre a data inicial e final
            // Apos a filtragem é calculada a soma das vendas totais do metodo Amount
        }
    }
}
