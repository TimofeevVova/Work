using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("employee")]
    public class Employee  // [Required]  представляет аннотацию, которая указывает, что свойство name обязательно должно иметь значение.
    {
        [JsonIgnore]
        [Required]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

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
        [Column("department")]
        public string Department { get; set; }

        [Required]
        [Column("salary")]
        public int Salary { get; set; }

        [Required]
        [Column("contract")]
        public string Contract { get; set; }
    }
}