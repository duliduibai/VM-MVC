using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;
using VM.IRepository;

namespace VM.Service.ProductDB
{
    public class ProductService : ServiceBase ,IProductService
    {
        public IProductRepository db { get; private set; }
        public ProductService(IProductRepository db)
        {
            this.db = db;
            this.AddDisposableObject(db);
        }
        public Product GetMovie(string productId)
        {
            return db.GetProduct(productId);
        }

        public IEnumerable<Product> GetMovies(int pageIndex, int pageSize, out int recordCount)
        {
            return db.GetProducts(pageIndex, pageSize, out recordCount);
        }

        public IEnumerable<Product> GetMoviesByActor(string actor, int pageIndex, int pageSize, out int recordCount)
        {
            return db.GetProductsByActor(actor, pageIndex, pageSize, out recordCount);
        }

        public IEnumerable<Product> GetMoviesByGenre(string genre, int pageIndex, int pageSize, out int recordCount)
        {
            return db.GetProductsByGenre(genre, pageIndex, pageSize, out recordCount);
        }

        public int GetStock(string proId)
        {
            ///TODO: verify the grammer
            return db.GetProduct(proId)?.Stock ?? 0;
        }
    }
}
