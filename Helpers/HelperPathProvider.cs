
namespace MvcCoreCacheRedis.Helpers
{
    public class HelperPathProvider
    {
        public enum Folders { Images, Documents }
        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment) {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder) {
            string path = "";
            switch (folder) {
                case Folders.Images:
                    path = "images";
                    break;
                case Folders.Documents:
                    path = "documents";
                    break;
            }
            path = Path.Combine(this.hostEnvironment.WebRootPath, path, fileName);
            return path;
        }
    }
}
