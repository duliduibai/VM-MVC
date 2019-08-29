using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM.Entity;
using VM.Service.ProductDB;
using VM_MVC.Models;

namespace VM_MVC.Controllers
{
    public class ProductController : VmController
    {
        public IProductService ProductService { get; private set; }

        private static int pageIndex { get; set; }

        public ProductController(IProductService productService)
        {
            this.ProductService = productService;
            this.AddDisposableObject(productService);
        }
        /// <summary>
        /// 影片列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Index(int pageIndex = 1)
        {
            ProductController.pageIndex = pageIndex;
            int recordCount;
            IEnumerable<GeneralMovieInfo> movies = this.ProductService
                .GetMovies(pageIndex, PagingInfo.PageSize, out recordCount)
                .Select(p => GeneralMovieInfo.FromProduct(p));
            var temp = movies.ToList();
            Func<int, UrlHelper, string> pageUrlAccessor = (CurrentPage, helper) => 
                helper.RouteUrl("Page", new { PageIndex = CurrentPage }).ToString();
            ViewBag.Title = "Video Mall";

            return RenderMovieList(movies, recordCount, pageIndex, pageUrlAccessor);
        }
        /// <summary>
        /// 根据导演显示影片
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Actor(string actor, int pageIndex = 1)
        {
            int recordCount;
            IEnumerable<GeneralMovieInfo> movies = this.ProductService
                .GetMoviesByActor(actor, pageIndex, PagingInfo.PageSize, out recordCount)
                .Select(p => GeneralMovieInfo.FromProduct(p));

            Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) =>
                helper.RouteUrl("ActorPage", new { PageIndex = currentPage }).ToString();

            ViewBag.Title = actor;
            return RenderMovieList(movies, recordCount, pageIndex, pageUrlAccessor);
        }

        /// <summary>
        /// 根据类型显示影片
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Genre(string genre, int pageIndex = 1)
        {
            int recordCount;
            IEnumerable<GeneralMovieInfo> movies = this.ProductService
                .GetMoviesByGenre(genre, pageIndex, PagingInfo.PageSize, out recordCount)
                .Select(p => GeneralMovieInfo.FromProduct(p));
            var temp = movies.Count();
            Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) =>
                helper.RouteUrl("GenrePage", new { PageIndex = currentPage }).ToString();

            ViewBag.Title = genre;
            return RenderMovieList(movies, recordCount, pageIndex, pageUrlAccessor);
        }

        /// <summary>
        /// 显示指定影片详细信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult Detail(string productId)
        {
            Product product = this.ProductService.GetMovie(productId);
            if (null == product)
            {
                return new HttpNotFoundResult(String.Format("指定的产品“{0}”不存在", productId));
            }
            return View(MovieInfo.FromProduct(product));
        }

        /// <summary>
        /// 显示Movie列表
        /// </summary>
        /// <param name="movies"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageUrlAccessor"></param>
        /// <returns></returns>
        public ActionResult RenderMovieList(IEnumerable<GeneralMovieInfo> movies, 
            int recordCount, int pageIndex, Func<int, UrlHelper, string> pageUrlAccessor)
        {
            ViewResult result = View("MovieList", movies);
            ViewBag.RecordCount = recordCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageUrlAccessor = pageUrlAccessor;
            return result;
        }

        [HttpPost]
        public ActionResult AddToCart(string productId, string productName,
            decimal price, [ModelBinder(typeof(ShoppingCartBinder))] ShoppingCart shoppingCart, int number = 0)
        {
            if (number != 0)
            {
                shoppingCart.Add(productId, productName, price, number);
            }
            return Index(pageIndex);
        }
    }
}