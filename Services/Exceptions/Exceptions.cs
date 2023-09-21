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
        public void ExceptionAgeMessage()
        {
            Console.WriteLine("Человеку меньше 18 лет");
        }

    }
    public class ExceptinoNoPassportData : Exception
    {

        public static void ExceptinoNoPassportDataMessage()
        {
            Console.WriteLine("Пасспортные данные не указаны");
        }
    }
}
