using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person //Class Человек    (Имя Фамилия Возраст Адрес)
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        private string PasportData { get; set; }

        public void SetPasportData(string data) { PasportData = data; }
        public string GetPasportData() { return PasportData; }
    }
}
