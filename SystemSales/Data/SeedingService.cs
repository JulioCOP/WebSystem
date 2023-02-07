using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSales.Models;
using SystemSales.Models.Enums;

namespace SystemSales.Data
{
    public class SeedingService //Operção é chamada em Startu, dentro do método Configure
    {
        // Implementar o serviço de dependência
        private SystemSalesContext _context;

        // Indicar que injeção de dependência deve ocorrer

        public SeedingService(SystemSalesContext context)
        {
            _context = context;
        }
        // Quando um SeedingService for criado, ele receberá uma instância vinda do context

        public void Seed() //operação para popular a base de dados
        {
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecords.Any())
            {
                return; // Verifica-se se o DB já foi populado, ou sejam se já possui dados
            }
            // Alimentar o banco de dados

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Smartphone");
            Department d3 = new Department(3, "Clothes");
            Department d4 = new Department(4, "Books");
            Department d5 = new Department(5, "Shoes");
            Department d6 = new Department(6, "Tools");
            Department d7 = new Department(7, "Eletronics");


            //Associação dos objetos
            Seller s1 = new Seller(1, "Bob Brow", "bob12@gmail.com", new DateTime(1998, 4, 21), 1200.0, d1);
            Seller s2 = new Seller(2, "Maria Green", "maria678@gmail.com", new DateTime(1979, 12, 31), 3500.0, d2);
            Seller s3 = new Seller(3, "Alex Grey", "greyalex@gmael.com", new DateTime(1988, 1, 15), 2200.0, d1);
            Seller s4 = new Seller(4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0, d4);
            Seller s5 = new Seller(5, "Donald Blue", "blue@gmail.com", new DateTime(2000, 1, 9), 1100.0, d3);
            Seller s6 = new Seller(6, "Alex Pink", "pinkalex@gmail.com", new DateTime(1997, 3, 4), 300.0, d2);
            Seller s7 = new Seller(7, "Jhon Yellow", "jhonny@gmail.com", new DateTime(1994, 5, 9), 2500.0, d7);
            Seller s8 = new Seller(8, "Sthephany Orange", "sthephanyo@gmial.com", new DateTime(1992, 7, 2), 2900.0, d5);
            Seller s9 = new Seller(9, "Ferdinand Oreo", "ferdinandoreo@gmail.com", new DateTime(1988, 12, 12), 3500.0, d6);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 09, 4), 7000.0, SaleStatus.Billed, s5);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2018, 09, 13), 4000.0, SaleStatus.Canceled, s4);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2018, 09, 1), 8000.0, SaleStatus.Billed, s1);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2018, 09, 21), 3000.0, SaleStatus.Billed, s3);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2018, 09, 15), 2000.0, SaleStatus.Billed, s1);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2018, 09, 28), 13000.0, SaleStatus.Billed, s2);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2018, 09, 11), 4000.0, SaleStatus.Billed, s4);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2018, 09, 14), 11000.0, SaleStatus.Pending, s9);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2018, 09, 7), 9000.0, SaleStatus.Billed, s6);
            SalesRecord r11 = new SalesRecord(11, new DateTime(2018, 09, 13), 6000.0, SaleStatus.Billed, s2);
            SalesRecord r12 = new SalesRecord(12, new DateTime(2018, 09, 25), 7000.0, SaleStatus.Pending, s8);
            SalesRecord r13 = new SalesRecord(13, new DateTime(2018, 09, 29), 10000.0, SaleStatus.Billed, s4);
            SalesRecord r14 = new SalesRecord(14, new DateTime(2018, 09, 4), 3000.0, SaleStatus.Billed, s5);
            SalesRecord r15 = new SalesRecord(15, new DateTime(2018, 09, 12), 4000.0, SaleStatus.Billed, s1);
            SalesRecord r16 = new SalesRecord(16, new DateTime(2018, 10, 5), 2000.0, SaleStatus.Billed, s8);
            SalesRecord r17 = new SalesRecord(17, new DateTime(2018, 10, 1), 12000.0, SaleStatus.Billed, s7);
            SalesRecord r18 = new SalesRecord(18, new DateTime(2018, 10, 24), 6000.0, SaleStatus.Billed, s3);
            SalesRecord r19 = new SalesRecord(19, new DateTime(2018, 10, 22), 8000.0, SaleStatus.Billed, s5);
            SalesRecord r20 = new SalesRecord(20, new DateTime(2018, 10, 15), 8000.0, SaleStatus.Billed, s6);
            SalesRecord r21 = new SalesRecord(21, new DateTime(2018, 10, 17), 9000.0, SaleStatus.Billed, s2);
            SalesRecord r22 = new SalesRecord(22, new DateTime(2018, 10, 24), 4000.0, SaleStatus.Billed, s4);
            SalesRecord r23 = new SalesRecord(23, new DateTime(2018, 10, 19), 11000.0, SaleStatus.Canceled, s7);
            SalesRecord r24 = new SalesRecord(24, new DateTime(2018, 10, 12), 8000.0, SaleStatus.Billed, s9);
            SalesRecord r25 = new SalesRecord(25, new DateTime(2018, 10, 31), 7000.0, SaleStatus.Billed, s3);
            SalesRecord r26 = new SalesRecord(26, new DateTime(2018, 10, 6), 5000.0, SaleStatus.Billed, s4);
            SalesRecord r27 = new SalesRecord(27, new DateTime(2018, 10, 13), 9000.0, SaleStatus.Pending, s1);
            SalesRecord r28 = new SalesRecord(28, new DateTime(2018, 10, 7), 4000.0, SaleStatus.Billed, s9);
            SalesRecord r29 = new SalesRecord(29, new DateTime(2018, 10, 23), 12000.0, SaleStatus.Billed, s5);
            SalesRecord r30 = new SalesRecord(30, new DateTime(2018, 10, 12), 5000.0, SaleStatus.Billed, s2);

            //Adicionar os objetos no banco de dados

            _context.Department.AddRange(d1, d2, d3, d4, d5, d6, d7);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6, s7, s8, s9);
            _context.SalesRecords.AddRange(
                r1, r2, r3, r4, r5, r6, r7, r8, r9, r10,
                r11, r12, r13, r14, r15, r16, r17, r18, r19, r20,
                r21, r22, r23, r24, r25, r26, r27, r28, r29, r30
            );

            _context.SaveChanges(); //para adicionar e salvar no banco de dados

        }
    }
}
