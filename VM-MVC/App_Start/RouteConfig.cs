using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VM_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ///主页【影片列表（第1页）】
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Product", action = "Index", pageIndex = 1 });

            ///主页（影片列表【第n页】）
            ///TODO:???
            routes.MapRoute(
                name : "Page",
                url:"Page{pageIndex}",
                defaults: new { controller = "Product", action = "Index", pageIndex = 1 },
                constraints: new { pageIndex = @"\d+"});

            ///基于指定类型的影片列表
            routes.MapRoute(
                name: "GenreHome",
                url: "Genre/{genre}",
                defaults: new { controller = "Product", action = "Genre", pageIndex = 1 });

            ///TODO:???
            routes.MapRoute(
                name: "GenrePage",
                url: "Genre/{genre}/Page{pageIndex}",
                defaults: new { controller = "Product", action = "Genre", pageIndex = 1 });

            routes.MapRoute(
                name: "AddToCart",
                url: "Product/AddToCart",
                defaults: new { controller = "Product", action = "AddToCart" });

            ///由指定演员参演的影片列表（第1页）
            routes.MapRoute(
                name: "ActorHome",
                url: "Actor/{Actor}",
                defaults: new { controller = "Product", action = "Actor", pageIndex = 1 });

            routes.MapRoute(
                name: "ActorPage",
                url: "Actor/{actor}/Page{pageIndex}",
                defaults: new { controller = "Product", action = "Actor", pageIndex = 1 });

            ///影片详细信息
            routes.MapRoute(
                name: "ProductDetail",
                url: "{productName}/{productId}",
                defaults: new { controller = "Product", action = "Detail" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });
            ///购物车
            routes.MapRoute(
                name: "ShoppingCart",
                url: "ShoppingCart",
                defaults: new { controller = "Order", action = "ShoppingCart" });

            ///从购物车一处选购商品
            routes.MapRoute(
                name: "Remove",
                url: "ShoppingCart/Remove",
                defaults: new { controller = "Order", action = "Remove" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRoute(
                name: "UpdateMovieNumber",
                url: "ShoppingCart/UpdateNumber",
                defaults: new { controller = "Order", action = "UpdateNumber" });

            ///结账支付
            routes.MapRoute(
                name: "CheckOut",
                url: "CheckOut",
                defaults: new { controller = "Order", action = "CheckOut" });

            ///登陆
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Account", action = "Login" });

            ///登陆2
            routes.MapRoute(
                name: "UserLogin",
                url: "UserLogin",
                defaults: new { controller = "Account", action = "UserLogin" });
            ///获取公钥
            routes.MapRoute(
                name: "GetRSAPubKey",
                url: "GetRSAPubKey",
                defaults: new { controller = "Account", action = "GetRSAPubKey" });
            ///验证
            routes.MapRoute(
                name: "Authenticate"
                , url: "Account/Authenticate"
                , defaults: new { controller = "Account", action = "Authenticate" });
            ///注册
            routes.MapRoute(
                name: "Register"
                , url: "Account/Register"
                , defaults: new { controller = "Account", action = "Register" });
            ///注销登陆
            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Account", action = "Logout" });
            ///TEST
            routes.MapRoute(
                name: "Test",
                url: "Test",
                defaults: new { controller = "Account", action = "Test" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
