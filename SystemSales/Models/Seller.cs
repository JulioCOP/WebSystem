using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SystemSales.Models
{
    public class Seller
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public double BaseSalary { get; set; }

        // Criação dos departamentos cada vendedor possui apenas um unico departamento

        public Department Department { get; set; }

        // Associaçã de 1 vendedor, possui pode possior varias vendas
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
