using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /*
    // V1.0
    public class Account : Client // банковский счет клиента (Основной Айди Валюта Сумма)
    {
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
        public int Amount { get; set; }
        public int OwnerId { get; set; } // id клиента данного аккаунта
    }
    */
    // V2.0
    [Table("account")]
    public class Account
    {
        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required]
        [Column("currency")] // валюта
        public string Currency { get; set; }

        [Column("amount")] // количество
        public int Amount { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public Client Owner { get; set; }

    }
}