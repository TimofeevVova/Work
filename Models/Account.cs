using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("account")]
    public class Account  // [Required]  представляет аннотацию, которая указывает, что свойство name обязательно должно иметь значение.
    {
        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required]
        [Column("currency")]
        public string Currency { get; set; }

        [Required]
        [Column("amount")]
        public double Amount { get; set; }

        [Required]
        [Column("owner_id")]
        [ForeignKey("Client")]
        public int OwnerId { get; set; }

        public Client Client { get; set; }

    }
}