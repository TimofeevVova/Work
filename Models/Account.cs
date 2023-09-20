using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account : Client // банковский счет клиента (Валюта Сумма)
    {
        public Currency Currency { get; set; }
        public int Amount { get; set; }

    }
}