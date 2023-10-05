using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Exceptions
{
    //(в проект Services добавить папку Exceptions, в которую добавить собственный класс исключения);
    public class ExceptionAge : Exception
    {
        public ExceptionAge() : base("Человеку меньше 18 лет")
        {
        }
    }

    public class ExceptionNoPassportData : Exception
    {
        public ExceptionNoPassportData() : base("Пасспортные данные не указаны")
        {
        }
    }
}
