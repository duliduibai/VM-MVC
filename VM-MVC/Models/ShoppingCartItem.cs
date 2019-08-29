using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VM_MVC.Models
{
    /// <summary>
    /// 购物车明细
    /// </summary>
    [Serializable]
    public class ShoppingCartItem
    {
        [DisplayName("数量")]
        public decimal Quantity { get; set; }
        [DisplayName("单价")]
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        [DisplayName("片名")]
        public string ProductName { get; set; }
        [DisplayName("金额")]
        public decimal SubTotalPrice
        {
            get
            {
                return this.Quantity * this.Price;
            }
        }
    }
}