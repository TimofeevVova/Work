﻿using HelloApp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start program");

            using (var db = new ApplicationContext())
            {
                // Выводим всех клиентов из базы данных
                var allClients = db.clientData.ToList();
                Console.WriteLine("Список всех клиентов в базе данных:");

                foreach (var client in allClients)
                {
                    Console.WriteLine($"ID: {client.Id} Имя: {client.Name}, Фамилия: {client.Famify} Адрес: {client.Address}, ДатаРожд: {client.Date.ToString("yyyy-MM-dd")}, Email: {client.Email}");
                }
            }

            Console.ReadKey();
        }
    }
}