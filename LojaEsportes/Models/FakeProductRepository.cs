using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaEsportes.Models
{
    public class FakeProductRepository //: IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product{ Name = "Jaqueta", Price = 250 },
            new Product{ Name = "Bola", Price = 150 },
            new Product{ Name = "Skate", Price = 200 },
            new Product{ Name = "Tenis", Price = 150 }
        }.AsQueryable<Product>();
    }
}
