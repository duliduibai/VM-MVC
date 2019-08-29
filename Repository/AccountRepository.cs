using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Entity;
using VM.IRepository;

namespace VM.Repository
{
    public class AccountRepository : VmRepository<Client>, IAccountRepository
    {
        public AccountRepository(VmDbContext context) : base(context)
        {

        }

        /// <summary>
        /// 获取Client
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Client GetClientByNameAndPwd(string userName, string password)
        {
            var client = DbSet.FirstOrDefault(m => m.UserName == userName);
            if (client.Password != password)
            {
                client = null;
            }
            return client;
        }

        /// <summary>
        /// 添加Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool AddClient(Client client)
        {
            DbSet.Attach(client);
            DbContext.Entry(client).State = System.Data.Entity.EntityState.Added;
            DbContext.SaveChanges();
            return true;
        }

        public Client GetClientByName(string userName)
        {
            var client = DbSet.FirstOrDefault(m => m.UserName == userName);
            return client;
        }

        public bool UpdateClient(Client client)
        {
            if (DbSet.Any(m=>m==client))
            {
                DbContext.Entry(client).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
