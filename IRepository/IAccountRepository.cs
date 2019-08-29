using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;

namespace VM.IRepository
{
    public interface IAccountRepository
    {
        bool AddClient(Client client);
        Client GetClientByNameAndPwd(string userName, string password);
        Client GetClientByName(string userName);
        bool UpdateClient(Client client);
    }
}
