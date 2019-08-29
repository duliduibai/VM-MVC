using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.Service.OrderDB
{
    public interface IOrderService
    {
        void SubmitOrder(Order order);
    }
}
