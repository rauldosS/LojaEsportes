using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaEsportes.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;

        public Product DeleteProduct(int productID)
        {
            // Busca produto a ser deletado do banco de dados
            Product dbEntry = context.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if(dbEntry != null) {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            // Se o ID é zero, um novo produto está sendo inserido no Entity Framework
            if (product.ProductID == 0) {
                context.Products.Add(product);
            } else // Se o ID não é zero, um produto está sendo editado
            {
                // Busca produto armazenado no banco de dados com os dados antigos
                Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                // Garante que o produto realmente existe na base de dados
                if (dbEntry != null) {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
