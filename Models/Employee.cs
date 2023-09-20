using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person //Class Сотрудник  (Айди Отделение Зарплата Контракт)
    {
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string Contract { get; set; }
    }
}
