using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LojaEsportes.Models;

namespace LojaEsportes.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent( IProductRepository repo ) {
            repository = repo;
        }

        public IViewComponentResult Invoke() {
            
            // Registra Menu ativo na ViewBag com base na variável de rota
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View( repository.Products
                            .Select( x => x.Category )
                            .Distinct()
                            .OrderBy( x => x )
                        );
        }
    }
}
