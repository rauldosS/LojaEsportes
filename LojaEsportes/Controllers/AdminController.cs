using Microsoft.AspNetCore.Mvc;
using LojaEsportes.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace LojaEsportes.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo) {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit (Product product) {
            if(ModelState.IsValid) {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} foi salvo com sucesso!";
                return RedirectToAction("Index");
            }
            else {
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productID) {
            Product deletedProduct = repository.DeleteProduct(productID);
            if(deletedProduct != null) {
                TempData["message"] = $"{deletedProduct.Name} foi removido com sucesso";
            }
            return RedirectToAction("Index");
        }
    }
}
