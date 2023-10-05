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
    public class Employee : Person //Class Сотрудник  (Айди Отделение Зарплата Контракт)
    {
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string Contract { get; set; }
    }
    */


    // V2.0
    [Table("employee")]
    public class Employee
    {
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
