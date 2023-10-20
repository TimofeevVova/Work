using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("client")]
    public class Client  // [Required]  представляет аннотацию, которая указывает, что свойство name обязательно должно иметь значение.
    {
        [JsonIgnore]
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
