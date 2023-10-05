using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public struct Currency //Struct Валюта   (Тип курс)
    {
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
    }
}
