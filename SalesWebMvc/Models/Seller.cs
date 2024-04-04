using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; } 
        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        //Sales é a lista de vendas associadas ao vendedor (Seller)
        public Seller()
        {
        } 
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Departament = departament;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr); //add na lista
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr); //removendo da lista
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
            //Todo objeto sr tal que sr.date seja maior ou igual a minha data inicial, e sr.date seja menor ou igual a minha data final. Agora quero a soma das vendas baseado em no atributo sr.amount

        }
    }
}
