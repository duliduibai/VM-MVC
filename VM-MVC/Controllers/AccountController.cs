using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using VM.Entity;
using VM.Service.AccountDB;

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
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            CryptoService.RSA_GeneratePEMKey();
            Response.Cookies.Add(new System.Web.HttpCookie("publicKey", CryptoService.PemPublicKey));
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public string Authenticate(string userName, string password)
        {
            CryptoService.RSA_GeneratePEMKey();
            var decryptUserName = "";
            decryptUserName = Encoding.UTF8.GetString(CryptoService.RSA.Decrypt(Convert.FromBase64String(userName), false));
            password = Encoding.UTF8.GetString(CryptoService.RSA.Decrypt(Convert.FromBase64String(password), false));
            var logError = service.Login(decryptUserName, CryptoService.MD5_Encrypt(password));
            if (String.IsNullOrEmpty(logError))
            {
                bLoginSuc = true;
                Session[decryptUserName] = decryptUserName;
                Response.Cookies.Add(new System.Web.HttpCookie("User_SessionId", userName));
                ViewBag.IsLogin = true;
            }
            return logError;
        }

        [HttpPost]
        public string Register(Client client)
        {
            var password = Encoding.UTF8.GetString(CryptoService.RSA.Decrypt(Convert.FromBase64String(client.Password), false));
            client.Password = CryptoService.MD5_Encrypt(password);
            client.UserName = Encoding.UTF8.GetString(CryptoService.RSA.Decrypt(Convert.FromBase64String(client.UserName), false));
            var regError = service.Register(client);
            return regError;
        }
    }
    public class Test
    {

    }
}