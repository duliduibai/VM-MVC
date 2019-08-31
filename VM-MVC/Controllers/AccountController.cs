using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VM.Entity;
using VM.Service.AccountDB;
using VM.Tools;

namespace VM_MVC.Controllers
{
    public class AccountController : VmController
    {
        public static bool bLoginSuc = false;
        private const string RSAKeyContainerName = "RSAKey";
        IAccountService service;
        public AccountController(IAccountService accountService)
        {
            this.service = accountService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Authenticate(string userName, string password, string key)
        {
            string URL = Url.RouteUrl("Home");
            var privateKey = VMEncrypt.CacheGet(key) as string;
            if (!String.IsNullOrEmpty(privateKey))
            {
                if (String.IsNullOrEmpty(userName))
                {
                    return Json(new { result = false, message = "用户名为空" }, JsonRequestBehavior.AllowGet);
                }
                if (String.IsNullOrEmpty(password))
                {
                    return Json(new { result = false, message = "密码为空" }, JsonRequestBehavior.AllowGet);
                }
                string decPwd = VMEncrypt.DecryptRSA(password, privateKey);
                string decUserName = VMEncrypt.DecryptRSA(userName, privateKey);
                ///连续两次MD5加密
                string md5Pwd = CryptoService.MD5_Encrypt(CryptoService.MD5_Encrypt(decPwd));
                var LoginClient = new Client();
                var logError = service.Login(decUserName, md5Pwd,out LoginClient);
                if (String.IsNullOrEmpty(logError))
                {
                    HttpCookie cookie = new HttpCookie("vm_login");
                    var sessionId = Guid.NewGuid().ToString();
                    //Session[decUserName] = decUserName;
                    Session.Add(sessionId, LoginClient);
                    cookie["session_id"] = sessionId;
                    ViewBag.IsLogin = true;
                    Response.Cookies.Add(cookie);
                    if (!String.IsNullOrEmpty(URL))
                    {
                        return Json(new { result = true, message = "成功", url = URL }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = true, message = "成功", url = URL }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { result = false, message = logError, url = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { result = false, message = "非法秘钥", url = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Register(Client client, string key)
        {
            string URL = Url.RouteUrl("Home");
            var privateKey = VMEncrypt.CacheGet(key) as string;
            if (!String.IsNullOrEmpty(privateKey))
            {
                client.Password = VMEncrypt.DecryptRSA(client.Password, privateKey);
                client.UserName = VMEncrypt.DecryptRSA(client.UserName, privateKey);
                client.Password = CryptoService.MD5_Encrypt(CryptoService.MD5_Encrypt(client.Password));
                var regError = service.Register(client);
                if (String.IsNullOrEmpty(regError))
                {
                    return Json(new { result = true, message = "成功", url = URL });
                }
                else
                    return Json(new { result = false, message = regError, url = URL });
            }
            else
            {
                return Json(new { result = false, message = "非法秘钥", url = URL });
            }
        }

        [HttpGet]
        public JsonResult GetRSAPubKey()
        {
            var pemPubKey = VMEncrypt.CreateRSAKey();
            return Json(new { Code = 0, RsaPublicKey = pemPubKey }, JsonRequestBehavior.AllowGet);
        }
    }
}