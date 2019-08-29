using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using VM.Entity;

namespace VM_MVC.Models
{
    public class GeneralMovieInfo
    {
        public static GeneralMovieInfo FromProduct(Product product)
        {
            return new GeneralMovieInfo
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Genre = product.Genre.Split('|'),
                Starring = product.Starring.Split('|'),
                Director = product.Director,
                ReleaseYear = product.ReleaseYear,
                Language = product.Language,
                Poster = product.Poster,
                Price = product.Price,
                StoryAbstract = product.StoryAbstract
            };
        }
        /// <summary>
        /// ID
        /// </summary>
        public string ProductID { get; set; }
        [DisplayName("影片名称")]
        public string Name { get; set; }
        [DisplayName("影片类型")]
        public IEnumerable<string> Genre { get; set; }
        [DisplayName("领衔主演")]
        public IEnumerable<string> Starring { get; set; }
        [DisplayName("导演")]
        public string Director { get; set; }
        [DisplayName("上映年份")]
        public int ReleaseYear { get; set; }
        [DisplayName("语言")]
        public string Language { get; set; }
        [DisplayName("单价")]
        public decimal Price { get; set; }
        [DisplayName("电影海报图片名称")]
        public string Poster { get; set; }
        [DisplayName("剧情简介（摘要）")]
        public string StoryAbstract { get; set; }
    }
}