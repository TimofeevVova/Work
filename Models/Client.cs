using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    // V1.0
    /*
    public class Client : Person //Class Клиент  (Айди Маил Телефон)
    {
        public List<int> IdAccounts { get; set; } // айди всех аккаунтов данного клиента
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // переопределение
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Client other = (Client)obj;
            return ClientId == other.ClientId;
        }

        public override int GetHashCode()
        {
            return ClientId.GetHashCode();
        }
    }
    */
    // V2.0
    [Table("client")]
    public class Client  // [Required]  представляет аннотацию, которая указывает, что свойство Name обязательно должно иметь значение.
    {
        [Required]
        [Column("client_id")]
        public int ClientId { get; set; }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("date_of_birth", TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Required]
        [Column("passport_data")]
        public string PassportData { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
