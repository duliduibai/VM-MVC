using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Entity
{
    /// <summary>
    /// 用户财产总览
    /// </summary>
    public class Account
    {
        public List<AccountLine> AccountLines { get; set; }
        public Account()
        {
            AccountLines = new List<AccountLine>();
        }
        [Key]
        public int AccountId { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime LastCostTime { get; set; }
    }
}
