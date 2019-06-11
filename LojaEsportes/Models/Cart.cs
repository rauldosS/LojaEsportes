using System.Collections.Generic;
using System.Linq;

namespace LojaEsportes.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection =
            new List<CartLine>();

        public virtual void AddItem( Product product, int quantity ) {

            // Busca linha de produto caso já exista no carrinho
            CartLine line = lineCollection
                            .Where(p => p.Product.ProductID == product.ProductID)
                            .FirstOrDefault();
            // Se line é nulo, produto ainda não existe no carrinho
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else // Senão, pega a linha do produto que já tem no carrinho e soma a nova quantidade
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        

        public virtual decimal ComputeTotalValue() => 
            lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
