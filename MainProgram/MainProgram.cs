using HelloApp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Helpers
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            ClientService clientService = new ClientService();
            Console.WriteLine("Start program");

            Console.WriteLine("Создание даных");
            //TestDataGenerator.GenerationClients(1);

            Console.WriteLine("Данные таблицы");
            using (var db = new ApplicationContext())
            {
                // Выводим всех клиентов из базы данных
                var allClients = db.clientData.ToList();
                Console.WriteLine("Список всех клиентов в базе данных:");

                foreach (var client in allClients)
                {
                    Console.WriteLine($"ID: {client.ClientId} Имя: {client.FirstName}, Фамилия: {client.LastName} Адрес: {client.Address}, ДатаРожд: {client.DateOfBirth.ToString("yyyy-MM-dd")}, Email: {client.Email}, Phone: {client.PhoneNumber}");
                }
            }

            int clientId = 11;

            Client client1 = new Client();
            Console.WriteLine($"Поиск клиента по Id = {clientId}");
            client1 = clientService.GetClient(clientId);
            // вывод данных в консоль
            clientService.ShowClientData(client1);

            // создание аккаунта клиенту
            //clientService.AddAccount(clientId);
 
            // изменение данных клиента
            Client changeClient = new Client
            {
                FirstName = "Анна",
                LastName = "Ильишна",
                DateOfBirth = new DateTime(2001, 1, 1),
                Address = "Беларусь",
                PassportData = "88005553535",
                Email = "noMail@mail.ru",
                PhoneNumber = "77900001"
            };
            clientService.UpdateClient(clientId, changeClient);
            // вывод данных в консоль
            clientService.ShowClientData(client1);

            // удаление клиента
            //clientService.RemoveClient(9);

            // удаление аккаунта
            //clientService.RemoveAccount(50);

            Console.ReadKey();
        }
    }
}
