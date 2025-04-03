using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedis.Repository;
using MvcCoreCacheRedis.Models;
using MvcCoreCacheRedis.Services;

namespace MvcCoreCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis service;

        public ProductosController(RepositoryProductos repo, ServiceCacheRedis service) {
            this.repo = repo;
            this.service = service;
        }

        public IActionResult Index() {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Detalle(int id) {
            Producto producto = this.repo.FindProducto(id);

            if (producto == null) {
                return NotFound();
            }
            return View(producto);
        }

        public async Task<IActionResult> Favoritos() {
            List<Producto> productos = await this.service.GetProductosFavoritosAsync();
            return View(productos);
        }

        public async Task<IActionResult> SeleccionarFavorito(int idproducto) {
            Producto producto = this.repo.FindProducto(idproducto);
            if (producto == null) {
                return NotFound();
            }

            await this.service.AddProductoFavoritoAsync(producto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFavorito(int idproducto) {
            await this.service.DeleteProductoFavoritoAsync(idproducto);
            return RedirectToAction("Favoritos");
        }

    }
}
