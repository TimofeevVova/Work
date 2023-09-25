using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account : Client // банковский счет клиента (Основной Айди Валюта Сумма)
    {
        public bool IsDefault { get; set; }
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
        public int Amount { get; set; }
        public int[] OwnerId { get; set; }

    }
}