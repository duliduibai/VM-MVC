using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Entity
{
    /// <summary>
    /// 记录账户流水
    /// </summary>
    public class AccountLine
    {
        [Key]
        public int AccountLineId { get; set; }
        /// <summary>
        /// 账户Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 流水对应的订单1:1
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 流水金额
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CostTime { get; set; }
    }
}
