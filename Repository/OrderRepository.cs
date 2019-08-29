using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;
using VM.IRepository;

namespace VM.Repository
{
    public class OrderRepository : VmRepository<Order> ,IOrderRepository
    {
        public OrderRepository(VmDbContext dbContext) : base(dbContext)
        { }

        public void AddOrder(Order order)
        {
            DbSet.Attach(order);
            this.DbContext.Entry(order).State = System.Data.Entity.EntityState.Added;
            foreach (OrderLine item in order.OrderLines)
            {
                this.DbContext.Entry(item).State = System.Data.Entity.EntityState.Added;
            }
            this.DbContext.SaveChanges();
        }
    }
}
