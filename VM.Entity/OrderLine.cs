using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Entity
{
    /// <summary>
    /// 订单子表
    /// </summary>
    public class OrderLine
    {
        [Key]
        public string OrderID { get; set; }
        /// <summary>
        /// 订购的产品
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 订购数量
        /// </summary>
        public decimal Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}
