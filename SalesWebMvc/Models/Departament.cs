using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departament 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Departament()
        {
        }
        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSellers(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
            // pegando cada vendedor da minha lista seller, chamo o total de vendedores e faço a soma para todos vendedores(.Sum)
        }
    }
}
