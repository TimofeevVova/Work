using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using MainProgram;

namespace Helpers
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            ClientService clientService = new ClientService();

            Console.WriteLine("Start program");

            // создание рандобных клиентов+аккаунтов
            //Console.WriteLine("Создание даных");
            //TestDataGenerator.GenerationClients(1);
            //TestDataGenerator.GenerationEmployees(1);

            /*
            // Выводим всех клиентов из базы данных
            Console.WriteLine("Данные таблицы");
            using (var db = new ApplicationContext())
            {
                var allClients = db.clientData.ToList();
                foreach (var client in allClients)
                {
                    Console.WriteLine($"ID: {client.ClientId} Имя: {client.FirstName}, Фамилия: {client.LastName} Адрес: {client.Address}, ДатаРожд: {client.DateOfBirth.ToString("yyyy-MM-dd")}, Email: {client.Email}, Phone: {client.PhoneNumber}");
                }
            }
            */
            //тест рабочих методов
            {
                //int clientId = 11;
                //Client client1 = new Client();
                //Console.WriteLine($"Поиск клиента по Id = {clientId}");
                //client1 = clientService.GetClient(clientId);
                //вывод данных в консоль
                //clientService.ShowClientData(client1);

                // добавление аккаунта клиенту
                //clientService.AddAccount(clientId);

                // изменение данных клиента
                /*Client changeClient = new Client
                {
                    FirstName = "Анна",
                    LastName = "Ильишна",
                    DateOfBirth = new DateTime(2001, 1, 1),
                    Address = "Беларусь",
                    PassportData = "88005553535",
                    Email = "noMail@mail.ru",
                    PhoneNumber = "77900001"
                };*/
                //clientService.UpdateClient(clientId, changeClient);
                // вывод данных в консоль
                //clientService.ShowClientData(client1);

                // удаление клиента
                //clientService.RemoveClient(9);

                // удаление аккаунта
                //clientService.RemoveAccount(50);
            }

            // Пример фильтрации по имени и сортировки по дате рождения
            Func<Client, bool> nameFilter = c => c.FirstName == "Лев" && (c.DateOfBirth > new DateTime(1990, 1, 1)); // условия фильтрации или null
            Func<Client, object> orderByDateOfBirth = c => c.ClientId; // порядок сортировки или null
            int page = 1; // выводимая страница
            int pageSize = 10; // размер страницы

            // получаем список сортированных клиентов по фильтру 
            List<Client> result = clientService.GetFilteredClients(nameFilter, orderByDateOfBirth, page, pageSize);


            // Работа с IDisposable
            var db = new ApplicationContext();
            Dispose testClass = new Dispose(db);
            testClass.StartOpenConnections();


            // Финализатор
            int nubber = 400;
            ConnectionAndMemory connectionAndMemory = new ConnectionAndMemory(nubber);
            connectionAndMemory.CreateConnectionsAndMemory(nubber);

            Console.WriteLine($"Total Allocated:{ConnectionAndMemory.TotalAllocated}");
            Console.WriteLine($"Total Freed: {ConnectionAndMemory.TotalFreed}");
            /*
            Зашли в ApplicationContext
            Запрос к БД
            Получили ответ
            Total Allocated:198532
            Total Freed: 0
            */



            Console.ReadKey();
        }
    }
}
