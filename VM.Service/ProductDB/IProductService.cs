using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.Service.ProductDB
{
    public interface IProductService
    {
        IEnumerable<Product> GetMovies(int pageIndex, int pageSize, out int recordCount);
        IEnumerable<Product> GetMoviesByGenre(string genre, int pageIndex, int pageSize, out int recordCount);
        IEnumerable<Product> GetMoviesByActor(string actor, int pageIndex, int pageSize, out int recordCount);
        Product GetMovie(string productId);
        /// <summary>
        /// 库存
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        int GetStock(string proId);
    }
}
