using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;
using VM.IRepository;
using VM.Service.ProductDB;

namespace VM.Service.OrderDB
{
    public class OrderService : ServiceBase, IOrderService
    {
        public IOrderRepository db { get; private set; }
        public IProductService proSerivce { get; private set; }
        public OrderService(IOrderRepository db, IProductService productService)
        {
            this.db = db;
            this.proSerivce = productService;
            this.AddDisposableObject(db);
            this.AddDisposableObject(productService);
        }

        public void SubmitOrder(Order order)
        {
            db.AddOrder(order);
        }

        private void CheckStock(Order order)
        {
            foreach (var line in order.OrderLines)
            {
                if (this.proSerivce.GetStock(line.ProductID) < line.Quantity)
                {
                    ///TODO: Out of stock Exception
                    throw new Exception("Out Of Stock");
                }
            }
        }
    }
}
