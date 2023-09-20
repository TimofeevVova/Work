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

namespace PracticeWithTypes
{
    internal class Program_PracticeWithTypes
    {
        static void Main(string[] args)
        {

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
            /*
            //генерация коллекции клиент-банковский счет
            var DictionatyClientAccount = TestDataGenerator.CreateDictionaryClientAccount(GenerationClients);
            
            //генерация коллекции клиент- несколько банковских счетов
            var DictionatyClientAccountList = TestDataGenerator.CreateDictionaryClientAccountList(GenerationClients);
            */
            
            // запрос на поиск аккауна по клиенту
            var findAccountFromClient = EquivalenceTests.GetHashCodeNecessityPositivTest(count);
            Console.ReadKey();
        }
    }


    


}
