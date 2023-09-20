using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Services;
using Models;

namespace ServiceTests
{
    public class EquivalenceTests
    {
        static Random random = new Random();
        
        // создать нового клиента, данные которого есть в словаре и попробовать получить данные аккаунта по этому клиенту как ключу
        public static Account GetHashCodeNecessityPositivTest(int count)
        {            
            // количество данных
            
            //генерация кликентов
            var GenerationClients = TestDataGenerator.Generation1000Clients(count);
            //генерация коллекции клиент-банковский счет
            var DictionatyClientCurrency = TestDataGenerator.CreateDictionaryClientAccount(GenerationClients);

            Account account = new Account();

            //Создание рандомного клиента
            int value = random.Next(1, count);
            Client newClient = new Client();
            foreach (Client client in GenerationClients)
            {
                if(client.ClientId == value)
                {
                    newClient.FirstName = client.FirstName;
                    newClient.LastName = client.LastName;
                    newClient.Age = client.Age;
                    newClient.Address = client.Address;
                    newClient.ClientId = client.ClientId;
                    newClient.Email = client.Email;
                    newClient.PhoneNumber = client.PhoneNumber;
                }
            }

            Console.WriteLine($"Новый клиент: Имя -{newClient.FirstName} Фамилия - {newClient.LastName} Айди - {newClient.ClientId} Маил - {newClient.Email}");

            // поиск аккаунта по клиенту (изначально получил ошибку, затем переопределил метода Equals и GetHashCode)
            if (DictionatyClientCurrency.TryGetValue(newClient, out account))
            {
                Console.WriteLine($"Найден аккаунт для клиента {newClient.ClientId}:");
                Console.WriteLine($"Баланс аккаунта - {account.Amount}");
            }
            else
            {
                Console.WriteLine($"Аккаунт не найден для клиента {newClient.ClientId}");
            }
            return account;
        }

        //усложнить задачу на случай, когда у клиента несколько банковских счетов;
        //Реализовать аналогичный тест для списка - List<Emloyee>
    }
}
