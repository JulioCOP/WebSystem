using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSales.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //  Criação das associações -  1 departamento possui varios vendedores
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {


        }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
        // Método para adicionar vendedor
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        //Método para calculo do total de vendas no intervalo de datas
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Calculo do total de vendas do departamento para o intervalo das datas
            // Seránecessário pega a lista de vendas dos vendedores e somar com as vendas de cada vendeor

            return Sellers.Sum(seller => seller.TotalSales(initial, final));

            // Lista sellers para pega a lista de vendedores do departamento 
            // Argumento seller (para cada vendedor da lista) é aplicado a Expressão lambda para o total de vendas daquele deterinado periodo
            // Para que no final seja somada as vendas de cada departamento

        }
    }
}
