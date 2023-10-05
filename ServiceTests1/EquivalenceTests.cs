using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Services;
using Models;
using System.Collections;

namespace ServiceTests
{
    public class EquivalenceTests
    {
        // не актуально
        /*
        static Random random = new Random();

        // создать нового клиента, данные которого есть в словаре и попробовать получить данные аккаунта по этому клиенту как ключу
        //усложнить задачу на случай, когда у клиента несколько банковских счетов;
        public static List<Account> GetHashCodeNecessityPositivTest(int count)
        {            
            //генерация кликентов
            var GenerationClients = TestDataGenerator.Generation1000Clients(count);
            
            //генерация коллекции клиент- несколько банковских счетов
            var DictionatyClientAccountList = TestDataGenerator.CreateDictionaryClientAccountList(GenerationClients);

            List<Account> Account = new List<Account>();

            //Создание рандомного клиента
            int value = random.Next(1, count);
            Client newClient = new Client();
            foreach (Client Client in GenerationClients)
            {
                if(Client.ClientId == value)
                {
                    newClient.FirstName = Client.FirstName;
                    newClient.LastName = Client.LastName;
                    newClient.DateOfBirth = Client.DateOfBirth;
                    newClient.Address = Client.Address;
                    newClient.ClientId = Client.ClientId;
                    newClient.Email = Client.Email;
                    newClient.PhoneNumber = Client.PhoneNumber;
                }
            }

            Console.WriteLine("\n");
            Console.WriteLine($"Новый клиент: Имя -{newClient.FirstName} Фамилия - {newClient.LastName} Айди - {newClient.ClientId} Маил - {newClient.Email}");

            // перебор и сохранение всех аккаунтов клиента
            if (DictionatyClientAccountList.TryGetValue(newClient, out List<Account> clientAccounts))
            {
                Console.WriteLine($"Найдены аккаунты для клиента {newClient.FirstName} {newClient.LastName}:");
                foreach (Account account2 in  clientAccounts)
                {
                    Console.WriteLine($"Id аккаунта - {account2.AccountId}");
                    Account.Add(account2);
                }
            }
            else
            {
                Console.WriteLine($"Аккаунты не найдены для клиента {newClient.ClientId}");
            }
            return Account;
        } 
        */
    }
}
