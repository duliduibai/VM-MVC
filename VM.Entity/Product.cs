using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Entity
{
    public class Product
    {
        public Product()
        {
            this.OrderLines = new HashSet<OrderLine>();
        }
        public virtual ICollection<OrderLine> OrderLines { get; set; }

        [Key]
        public string ProductID { get; set; }
        /// <summary>
        /// 影片名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 影片类型
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// 领衔主演
        /// </summary>
        public string Starring { get; set; }
        /// <summary>
        /// 主演
        /// </summary>
        public string SupportingActors { get; set; }
        /// <summary>
        /// 导演
        /// </summary>
        public string Director { get; set; }
        /// <summary>
        /// 编剧
        /// </summary>
        public string ScriptWriter { get; set; }
        /// <summary>
        /// 出品国家
        /// </summary>
        public string ProductionCountry { get; set; }
        /// <summary>
        /// 出品公司
        /// </summary>
        public string ProductCompany { get; set; }
        /// <summary>
        /// 上映年份
        /// </summary>
        public int ReleaseYear { get; set; }
        public string Language { get; set; }
        /// <summary>
        /// 片长
        /// </summary>
        public int RunTime { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 电影海报图片名称
        /// </summary>
        public string Poster { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 剧情（完整）
        /// </summary>
        public string Story { get; set; }
        /// <summary>
        /// 剧情简介（摘要）
        /// </summary>
        public string StoryAbstract { get; set; }
    }
}
