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
    public class Account  // [Required]  представляет аннотацию, которая указывает, что свойство Name обязательно должно иметь значение.
    {
        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required]
        [Column("currency")]
        public string Currency { get; set; }

        [Required]
        [Column("amount")]
        public int Amount { get; set; }

        [Required]
        [Column("owner_id")]
        [ForeignKey("Client")]
        public int OwnerId { get; set; }

        public Client Client { get; set; }

    }
}