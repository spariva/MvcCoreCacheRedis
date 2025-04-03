using MvcCoreCacheRedis.Helpers;
using MvcCoreCacheRedis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MvcCoreCacheRedis.Services
{
    public class ServiceCacheRedis
    {
        private IDatabase database;
        public ServiceCacheRedis() {
            this.database = HelperCacheMultiplexer.Connection.GetDatabase();
        }

        public async Task AddProductoFavoritoAsync(Producto producto) {
            string jsonProductos = await this.database.StringGetAsync("favoritos");
            List<Producto> productosList;

            if (jsonProductos == null) {
                productosList = new List<Producto>();
            }
            else {
                productosList = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            productosList.Add(producto);
            jsonProductos = JsonConvert.SerializeObject(productosList);
            await this.database.StringSetAsync("favoritos", jsonProductos);
        }

        public async Task<List<Producto>> GetProductosFavoritosAsync() {
            string jsonProductos = await this.database.StringGetAsync("favoritos");
            List<Producto> productosList;
            if (jsonProductos == null) {
                productosList = new List<Producto>();
            }
            else {
                productosList = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            return productosList;
        }

        public async Task DeleteProductoFavoritoAsync(int idProducto) {
            List<Producto> favs = await this.GetProductosFavoritosAsync();
            if (favs != null) {
                Producto productoDelete = favs.FirstOrDefault(x => x.IdProducto == idProducto);
                favs.Remove(productoDelete);

                if (favs.Count == 0) 
                {
                    await this.database.KeyDeleteAsync("favoritos");
                }
                else 
                {
                    string jsonProductos = JsonConvert.SerializeObject(favs);//lo de los 30 es opcional, sino es por def 24 horas
                    await this.database.StringSetAsync("favoritos", jsonProductos);

                    //await this.database.StringSetAsync("favoritos", jsonProductos, TimeSpan.FromMinutes(30));
                }
            }

        }


    }
}
