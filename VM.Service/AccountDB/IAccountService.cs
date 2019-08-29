using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.Service.AccountDB
{
    public interface IAccountService
    {
        string Login(string userName, string password);
        string Register(Client client);
        bool Update(Client client);
    }
}
