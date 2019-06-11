using System.ComponentModel.DataAnnotations;

namespace LojaEsportes.Models
{
    public class Product
    {
        public int ProductID { get; set;}
        [Required(ErrorMessage = "Informe o nome do produto")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Informe a descrição do produto")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Informe um valor positivo.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Informe a categoria do produto.")]
        public string Category { get; set; }
    }
}
