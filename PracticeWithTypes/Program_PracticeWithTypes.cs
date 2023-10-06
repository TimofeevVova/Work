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
            Dictionary<Client, List<Account>> ClientAccount = new Dictionary<Client, List<Account>>();
            // экземпляр класса, реализующего интерфейс IStorage
            IClientStorage storage2 = new ClientStorage();
            // вызов методов

            // добавление
            Client client1 = storage2.Add("Виолетта", "Воробъева", new DateTime(1999, 9, 18), "Тирасполь", "Viola555@mail.ru", "77504475", "88005553535");
            Client client2 = storage2.Add("Витя", "Воробъев", new DateTime(1999, 9, 18), "Тирасполь", "Viola555@mail.ru", "77504475", "88005553535");
            Client client3 = storage2.Add("Игорь", "Воробъев", new DateTime(1999, 9, 18), "Тирасполь", "Viola555@mail.ru", "77504475", "88005553535");
            Client client4 = storage2.Add("Иван", "Воробъев", new DateTime(1999, 9, 18), "Тирасполь", "Viola555@mail.ru", "77504475", "88005553535");

            // изменение клиента
            client1 = storage2.Update(client1, "Виолетта2", "Ивановна", new DateTime(1999, 5, 19), "Москва", "Viola505@mail.ru", "77504470", "8-800-555-35-35");

            // добавление аккаунта
            ClientAccount = storage2.AddAccount(client1);
            ClientAccount = storage2.AddAccount(client1);
            ClientAccount = storage2.AddAccount(client1);
            ClientAccount = storage2.AddAccount(client1);

            // изменение аккаунта
            ClientAccount = storage2.UpdateAccount(client1, 2, 5000);

            // удаление аккаунта
            Account account1 = ClientAccount[client1].FirstOrDefault(account => account.AccountId == 1);
            Client client20 = storage2.DeleteAccount(client1, account1);
            // удаление аккаунта
            Account account4 = ClientAccount[client1].FirstOrDefault(account => account.AccountId == 4);
            Client client21 = storage2.DeleteAccount(client1, account4);

            // удаление клиента
            storage2.Delete(client1);

            Console.ReadKey();
        }
    }





}
