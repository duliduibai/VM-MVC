using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.IRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int pageIndex, int pageSize, out int recordCount);
        IEnumerable<Product> GetProductsByGenre(string genre, int pageIndex, int pageSize, out int recordCount);
        IEnumerable<Product> GetProductsByActor(string actor, int pageIndex, int pageSize, out int recordCount);
        Product GetProduct(string productID);
    }
}
