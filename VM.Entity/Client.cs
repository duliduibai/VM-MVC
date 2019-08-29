using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Entity
{
    public class Client
    {
        public Client()
        {
            this.Orders = new List<Order>();
        }
        public virtual List<Order> Orders { get; set; }
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Comment { get; set; }
        public Nullable<DateTime> LastLoginTime { get; set; }
        public int WalletId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
