using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;
using VM.IRepository;

namespace VM.Service.AccountDB
{
    public class AccountService : ServiceBase, IAccountService
    {
        public IAccountRepository db;
        public AccountService(IAccountRepository db)
        {
            this.db = db;
            AddDisposableObject(db);
        }

        public string Login(string userName, string password)
        {
            Client client = db.GetClientByNameAndPwd(userName, password);
            if (client == null)
            {
                return "用户名或密码错误";
            }
            client.LastLoginTime = DateTime.Now;
            return "";
        }

        public string Register(Client client)
        {
            var temp = db.GetClientByName(client.UserName);
            if (temp == null)
            {
                client.LastLoginTime = DateTime.Now;
                db.AddClient(client);
                return "";
            }
            else
                return "用户名已经存在，请重新注册";
        }

        public bool Update(Client client)
        {
            return db.UpdateClient(client);
        }
    }
}
