using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using ServiceTests;
using System.Diagnostics;
using Services.Storage;

namespace PracticeWithTypes
{
    internal class Program_PracticeWithTypes
    {
        static void Main(string[] args)
        {

            // экземпляр класса, реализующего интерфейс IStorage
            IStorage storage2 = new ClientStorage(); 
            // вызов методов
            Client client1 = storage2.Add("Виолетта", "Воробъева", new DateTime(1999, 9, 18), "Тирасполь", "Viola555@mail.ru", "77504475", "88005553535");


            storage2.Update(client1,"Виолетта2", "Ивановна", new DateTime(1999, 5, 19), "Москва", "Viola505@mail.ru", "77504470", "8-800-555-35-35");











            Console.ReadKey();
        }
    }


    


}
