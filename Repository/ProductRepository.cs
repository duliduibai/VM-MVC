using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;
using VM.IRepository;

namespace VM.Repository
{
    public class ProductRepository : VmRepository<Product>, IProductRepository
    {
        public ProductRepository(VmDbContext dbContext) : base(dbContext)
        {

        }

        public Product GetProduct(string productID)
        {
            return this.DbSet.FirstOrDefault(p => p.ProductID == productID);
        }

        public IEnumerable<Product> GetProducts(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = this.DbSet.Count();
            return this.Get(p => true, pageIndex, pageSize, p => p.ReleaseYear, false);
        }

        public IEnumerable<Product> GetProductsByActor(string actor, int pageIndex, int pageSize, out int recordCount)
        {
            var sql = this.Get(p => p.Starring.Contains(actor) || p.SupportingActors.Contains(actor)
                , pageIndex, pageSize, p => p.ReleaseYear, false);
            recordCount = sql.Count();
            return sql;
        }

        public IEnumerable<Product> GetProductsByGenre(string genre, int pageIndex, int pageSize, out int recordCount)
        {
            var sql = this.Get(p => p.Genre.Contains(genre), pageIndex, pageSize, p => p.ReleaseYear, false);
            recordCount = sql.Count();
            return sql;
        }
    }

}
