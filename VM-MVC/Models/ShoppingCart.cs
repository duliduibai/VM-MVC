using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VM_MVC.Models
{
    /// <summary>
    /// 购物车
    /// </summary>
    [Serializable]
    public class ShoppingCart
    {
        public IList<ShoppingCartItem> Items { get; private set; }

        public decimal TotalQuantiy
        {
            get
            {
                return this.Items.Sum(item => item.Quantity);
            }
        }
        public decimal TotalPrice {
            get
            {
                return this.Items.Sum(item => item.Price * item.Quantity);
            }
        }
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public void Add(string productId, string productName, decimal price, int number)
        {
            ShoppingCartItem item = this.Items.FirstOrDefault(p => p.ProductId == productId);
            if (null != item)
            {
                item.Quantity += number;
            }
            else
            {
                this.Items.Add(new ShoppingCartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Quantity = number,
                    Price = price
                });

            }
        }
    }
}