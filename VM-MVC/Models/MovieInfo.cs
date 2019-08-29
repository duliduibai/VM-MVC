using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using VM.Entity;

namespace VM_MVC.Models
{
    public class MovieInfo
    {
        public static MovieInfo FromProduct(Product product)
        {
            return new MovieInfo
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Genre = product.Genre,
                Starring = product.Starring,
                SupportingActors = product.SupportingActors,
                Director = product.Director,
                ScriptWriter = product.ScriptWriter,
                ProductCompany = product.ProductCompany,
                ProductionCountry = product.ProductionCountry,
                ReleaseYear = product.ReleaseYear,
                Language = product.Language,
                Poster = product.Poster,
                RunTime = product.RunTime,
                Price = product.Price,
                Story = product.Story
            };
        }
        /// <summary>
        /// ID
        /// </summary>
        public string ProductID { get; set; }
        [DisplayName("影片名称")]
        public string Name { get; set; }
        [DisplayName("影片类型")]
        public string Genre { get; set; }
        [DisplayName("领衔主演")]
        public string Starring { get; set; }
        [DisplayName("主演")]
        public string SupportingActors { get; set; }
        [DisplayName("导演")]
        public string Director { get; set; }
        [DisplayName("编剧")]
        public string ScriptWriter { get; set; }
        [DisplayName("出品国家")]
        public string ProductionCountry { get; set; }
        [DisplayName("出品公司")]
        public string ProductCompany { get; set; }
        [DisplayName("上映年份")]
        public int ReleaseYear { get; set; }
        [DisplayName("语言")]
        public string Language { get; set; }
        [DisplayName("片长")]
        public int RunTime { get; set; }
        [DisplayName("单价")]
        public decimal Price { get; set; }
        [DisplayName("电影海报图片名称")]
        public string Poster { get; set; }
        [DisplayName("库存")]
        public int Stock { get; set; }
        [DisplayName("剧情（完整）")]
        public string Story { get; set; }
        [DisplayName("剧情简介（摘要）")]
        public string StoryAbstract { get; set; }
    }
}