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
            /*
            // Создание и отобравжение классов и структуры
            BasicMethods.FillingAndDisplaying();
            // Изменение данных
            BasicMethods.ChangeExchangeRate();
            // Расчет зарплаты директоров
            BasicMethods.SalaryCalculation();
            // преобразования клиента банка в сотрудника
            BasicMethods.ConvertClientToEmployee();
            // Учет времени упаковки и распаковки
            BasicMethods.TimeTracking();            
            
            
            int count = 5;
            //генерация кликентов
            var GenerationClients = TestDataGenerator.Generation1000Clients(count);
            
            //генерация коллекции по клиентам
            var DictionaryFromList = TestDataGenerator.CreateDictionaryFromList(GenerationClients);
            
            //генерация сотрудников
            var GenerationEmployee = TestDataGenerator.Generation1000Employee(count);
            
            //замер времени поиска клинента по номеру телефона из списка
            var timeFromSearchInList = TestDataGenerator.SearchByPhoneNumberInList(GenerationClients, count);
            
            //замер времени поиска клинента по номеру телефона из словаря
            var timeFromSearchInDictionary = TestDataGenerator.SearchByPhoneNumberInDictionary(DictionaryFromList, count);

            //поиск клиентов ниже определенного возраста
            int ageCount = 20;
            var ClientsFromAge = TestDataGenerator.FindAllClientsFromAge(GenerationClients, ageCount);
            
            //поиск сотрудника с минимальной зарплатой 
            int minSalary = 1000;
            var ClientMinSalary = TestDataGenerator.EmployeeMinSalary(GenerationEmployee, minSalary);

            //сравнение времени поиска последнего элемента списка
            TestDataGenerator.TwoSearchTypes(DictionaryFromList);
            
            //генерация коллекции клиент-банковский счет
            var DictionatyClientAccount = TestDataGenerator.CreateDictionaryClientAccount(GenerationClients);
            
            //генерация коллекции клиент- несколько банковских счетов
            var DictionatyClientAccountList = TestDataGenerator.CreateDictionaryClientAccountList(GenerationClients);
            
            
            // запрос на поиск аккауна по клиенту
            var findAccountFromClient = EquivalenceTests.GetHashCodeNecessityPositivTest(count);

            // создание нового клиента
            DateTime dateTime = new DateTime(1990, 1, 1);
            Dictionary<Client, List<Account>> Data =  ClientService.AddNewClient("Петя", "Быстров", dateTime, "Тирасполь", "345345345345", "Petya2245@mail.ru", "779556677");

            Client SomeClient = new Client();
            if (Data.Keys.Any())
            {
                // получаем список всех клиентов и берем последний из них
                List<Client> clientList = Data.Keys.ToList();
                SomeClient = clientList.Last();

                // редактирование первого аккаунта клиента
                List<Account> accounts = Data[SomeClient];
                Dictionary<Client, List<Account>> DataEdit = new Dictionary<Client, List<Account>>();
                DataEdit = ClientService.EditAccount(SomeClient);
            }


            //Data = ClientService.CreateAdditionalAccountFromClient(Petya);

            // добавление ещё одного аккаунта клиенту
            //ClientService.CreateAdditionalAccountFromClient(Petya);

            //показать данные всех аккаунтов6 клиента
            //TestDataGenerator.ViewDataClientAccounts(Data, Petya);

            

            Console.WriteLine("\n");

            //показать данные всех аккаунтов6 клиента
            TestDataGenerator.ViewDataClientAccounts(Data, SomeClient);

            */




            DateTime dateTime = new DateTime(1990, 1, 1);

            ClientStorage storage = new ClientStorage();
            dateTime = new DateTime(1990, 9, 18);
            storage.AddNewClient("Настя", "Воробъева", dateTime, "Тирасполь", "Nastya555@mail.ru", "779556677", "88005553535");

            dateTime = new DateTime(1985, 9, 18);
            storage.AddNewClient("Вика", "Воробъева", dateTime, "Тирасполь", "Nastya555@mail.ru", "779556677", "88005553535");

            dateTime = new DateTime(1986, 9, 18);
            storage.AddNewClient("Саша", "Воробъев", dateTime, "Тирасполь", "Nastya555@mail.ru", "779556677", "88005553535");

            dateTime = new DateTime(2004, 9, 18);
            storage.AddNewClient("Миша", "Воробъев", dateTime, "Тирасполь", "Nastya555@mail.ru", "779556677", "88005553535");

            dateTime = new DateTime(2005, 9, 18);
            storage.AddNewClient("Лера", "Воробъева", dateTime, "Тирасполь", "Nastya555@mail.ru", "779556677", "88005553535");

            ClientService clientService = new ClientService(storage);
            Client youngClient = clientService.GetYoungestClient();

            Console.WriteLine($"самый молодой - {youngClient.FirstName}");
            Console.WriteLine($"дата рождения - {youngClient.DateOfBirth}");


            Client oldClient = clientService.GetOldestClient();

            Console.WriteLine($"самый старый - {oldClient.FirstName}");
            Console.WriteLine($"дата рождения - {oldClient.DateOfBirth}");


            double years = clientService.GetAverageAge();
            Console.WriteLine($"средний возраст - {Convert.ToInt32(years)}");


            Console.ReadKey();
        }
    }


    


}
