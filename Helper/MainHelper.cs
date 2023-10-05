using BankContext;
using Models;

namespace Helpers
{
    internal class MainHelper
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start program");

            using (var db = new ApplicationContext())
            {
                // Выводим всех клиентов из базы данных
                var allClients = db.clientData.ToList();
                var allAccounts = db.accountData.ToList();
                var allEmployee = db.employeeData.ToList();
                Console.WriteLine("Список всех клиентов в базе данных:");

                foreach (var client in allClients)
                {
                    Console.Write("Клиенты:");
                    Console.WriteLine($"ID: {client.ClientId} Имя: {client.FirstName}, Фамилия: {client.LastName} Адрес: {client.Address}, ДатаРожд: {client.DateOfBirth.ToString("yyyy-MM-dd")}, Email: {client.Email}");
                }  
                
                foreach (var account in allAccounts)
                {
                    Console.Write("Аккаунты:");
                    Console.WriteLine($"ID: {account.AccountId} Тип: {account.Currency}, Баланс: {account.Amount}, OwnerId: {account.OwnerId} ");
                }
                
            }

            Console.ReadKey();
        }
    }
}
