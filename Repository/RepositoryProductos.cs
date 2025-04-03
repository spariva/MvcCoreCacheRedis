using MvcCoreCacheRedis.Helpers;
using static MvcCoreCacheRedis.Helpers.HelperPathProvider;
using System.Xml.Linq;
using MvcCoreCacheRedis.Models;

namespace MvcCoreCacheRedis.Repository
{
    public class RepositoryProductos
    {
        private XDocument document;


        public RepositoryProductos(HelperPathProvider helper) {

            string path = helper.MapPath("productos.xml", Folders.Documents);

            this.document = XDocument.Load(path);

        }

        public List<Producto> GetProductos() {
            var consulta = from prod in this.document.Descendants("producto")
                           select new Producto()
                           {
                               IdProducto = int.Parse(prod.Element("idproducto").Value),
                               Nombre = prod.Element("nombre").Value,
                               Descripcion = prod.Element("descripcion").Value,
                               Precio = (int)prod.Element("precio"),
                               Imagen = (string)prod.Element("imagen")
                           };

            return consulta.ToList();
        }

        public Producto FindProducto(int idProducto) {

            return this.GetProductos().FirstOrDefault(x => x.IdProducto == idProducto);

        }
    }
}
