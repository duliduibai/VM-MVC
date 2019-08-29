using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Entity
{
    public class Order
    {
        public Order()
        {
            this.OrderLines = new HashSet<OrderLine>();
        }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        [Key]
        public string OrderID { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime OrderTime { get; set; }

    }
}
