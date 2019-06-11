using System.Linq;

namespace LojaEsportes.Models
{
    public interface IProductRepository
    {
        // Estrutura de dados de produtos consultável
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
