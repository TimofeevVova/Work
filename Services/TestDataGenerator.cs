using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services
{
    public class TestDataGenerator
    {
        static Random random = new Random();

        //а) генерации коллекции 1000 клиентов банка;
        public static List<Client> Generation1000Clients(int count)
        {
            List<Client> clients = new List<Client>();

            for (int i = 0; i < count; i++)
            {
                Client client = new Client
                {
                    ClientId = i + 1,
                    FirstName = SettingsGenerator.GetRandomFirstName(),
                    LastName = SettingsGenerator.GetRandomName(),
                    DateOfBirth = SettingsGenerator.GetRandomDateOfBirth(),
                    Address = SettingsGenerator.GetRandomAddress(),
                    Email = SettingsGenerator.GetRandomEmail(),
                    PhoneNumber = SettingsGenerator.GetRandomPhoneNumber()
                };
                clients.Add(client);

                //Console.WriteLine($"Id - {client.ClientId}\nИмя - {client.FirstName}\nФамилия - {client.LastName}\nДата рождения - {client.DateOfBirth}\nАдрес - {client.Address}\nМаил - {client.Email}\nТелефон - {client.PhoneNumber}\n");
            }
            return clients;
        }

        //б) генерации словаря в качестве ключа которого применяется номер телефона клиента, в качестве значения сам клиент;
        public static Dictionary<string, Client> CreateDictionaryFromList(List<Client> clients)
        {
            Dictionary<string, Client> dictionary = new Dictionary<string, Client>();

            foreach (Client client in clients)
            {
                dictionary[client.PhoneNumber] = client;
            }
            return dictionary;
        }

        //в) генерации коллекции 1000 сотрудников банка.
        public static List<Employee> Generation1000Employee(int count)
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                Employee employee = new Employee
                {
                    EmployeeId = i + 1,
                    FirstName = SettingsGenerator.GetRandomFirstName(),
                    LastName = SettingsGenerator.GetRandomName(),
                    DateOfBirth = SettingsGenerator.GetRandomDateOfBirth(),
                    Address = SettingsGenerator.GetRandomAddress(),
                    Department = SettingsGenerator.GetRandomDepartment(),
                    Salary = SettingsGenerator.GetRandomSalary(),
                    Contract = SettingsGenerator.GetRandomContract(),
                };
                employees.Add(employee);

                //Console.WriteLine($"Id - {employee.EmployeeId}\nИмя - {employee.FirstName}\nФамилия - {employee.LastName}\nДатараждения - {employee.DateOfBirth}\nАдрес - {employee.Address}\nОтдел - {employee.Department}\nЗарплата - {employee.Salary}\nContract - {employee.Contract}");
            }
            return employees;
        }


        //а) пользуясь инструментом “Stopwatch”, провести замер времени выполнения поиска клиента по его номеру телефона среди элементов списка;
        // 55 тиков
        public static Client SearchByPhoneNumberInList(List<Client> clients, int count)
        {
            int value = random.Next(1, count);
            string phoneNumber = "";
            Client clientPhone = new Client();

            //поиск рандомного номера телефона
            foreach (Client client in clients)
            {
                if (client.ClientId == value)
                {
                    phoneNumber = client.PhoneNumber;
                    break;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //поиск по номеру телефона
            foreach (Client client in clients)
            {
                if (client.PhoneNumber == phoneNumber)
                {
                    clientPhone = client;
                    break;
                }
            }

            stopwatch.Stop();
            long searchTime = stopwatch.ElapsedTicks;
            Console.WriteLine("\n");
            Console.WriteLine($"Время поиска в списке - {searchTime} тиков");

            return clientPhone;
        }

        //б) провести замер времени выполнения поиска клиента по его номеру телефона, среди элементов словаря;
        // 109 тиков
        public static Client SearchByPhoneNumberInDictionary(Dictionary<string, Client> clients, int count)
        {
            int value = random.Next(1, count);
            string phoneNumber = "";
            Client clientPhone = new Client();

            //поиск рандомного номера телефона
            foreach (var kvp in clients)
            {
                if (kvp.Value.ClientId == value)
                {
                    phoneNumber = kvp.Value.PhoneNumber;
                    break;
                }
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //поиск по номеру телефона
            foreach (var kvp in clients)
            {
                if (kvp.Value.PhoneNumber == phoneNumber)
                {
                    clientPhone = kvp.Value;
                    break;
                }
            }

            stopwatch.Stop();
            long searchTime = stopwatch.ElapsedTicks;

            Console.WriteLine($"Время поиска в словаре - {searchTime} тиков");
            Console.WriteLine("\n");

            return clientPhone;
        }

        //в) выборку клиентов, возраст которых ниже определенного значения;        
        public static List<Client> FindAllClientsFromAge(List<Client> clients, int count)
        {
            List<Client> clientsFromAge = new List<Client>();

            DateTime currentDate = DateTime.Now; // Текущая дата            
            DateTime dateOfBirth = currentDate.AddYears(-count);

            foreach (Client client in clients)
            {
                if (client.DateOfBirth < dateOfBirth)
                {
                    clientsFromAge.Add(client);
                }
            }
            return clientsFromAge;
        }

        //г) поиск сотрудника с минимальной заработной платой;
        public static Employee EmployeeMinSalary(List<Employee> Employees, int minSalary)
        {
            Employee employeeWithMinSalaru = new Employee();

            foreach(Employee employee in Employees)
            {
                if(employee.Salary < minSalary)
                {
                    employeeWithMinSalaru = employee;
                    Console.WriteLine("\n");
                    Console.WriteLine($"Минимальная зарплата - {employee.Salary}");
                    Console.WriteLine("\n");
                    break;
                }
            }
            
            return employeeWithMinSalaru;
        }

        //д) сравнить скорость поиска по словарю двумя методами:
        public static void TwoSearchTypes(Dictionary<string, Client> clients)
        {
            //1) при помощи метода FirstOrDefault(ищем последний элемент коллекции);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var lastClient1 = clients.LastOrDefault();
            stopwatch.Stop();
            long searchTime1 = stopwatch.ElapsedTicks;

            stopwatch.Reset();

            //2) при помощи выборки по ключу последнего элемента коллекции.
            stopwatch.Start();
            if (clients.Count > 0)
            {
                var lastKey = clients.Keys.Last();
                var lastClient2 = clients[lastKey];
            }
            stopwatch.Stop();
            long searchTime2 = stopwatch.ElapsedTicks;
            Console.WriteLine("\n");
            Console.WriteLine($"Поиск с использованием FirstOrDefault занял {searchTime1} тиков.");
            Console.WriteLine($"Поиск по ключу занял {searchTime2} тиков.");
            Console.WriteLine("\n");
        }

        /*
        //реализовать метод, генерирующий словарь, где в качестве ключа находятся клиенты, а в качестве значения их банковский счет;
        public static Dictionary<Client, Account> CreateDictionaryClientAccount(List<Client> clients)
        {
            Dictionary<Client, Account> dictionary = new Dictionary<Client, Account>();
            Account account = new Account();            

            foreach (Client client in clients)
            {
                account = GenerateNewAccount();
                dictionary[client] = account;
                //Console.WriteLine($"{client.ClientId} - {account.Amount}");
            }
            return dictionary;
        }
        */

        // метод, генерирующий словарь, где в качестве ключа находятся клиенты, а в качестве значения несколько банковских счетов;
        public static Dictionary<Client, List<Account>> CreateDictionaryClientAccountList(List<Client> clients)
        {
            Dictionary<Client, List<Account>> dictionary = new Dictionary<Client, List<Account>>();

            foreach (Client client in clients)
            {
                List<Account> accounts = new List<Account>();
                accounts.Add(GenerateNewAccount());
                accounts.Add(GenerateNewAccount());
                dictionary[client] = accounts;

                Console.WriteLine($"Клиент {client.FirstName} {client.LastName} имеет {accounts.Count} аккаунты:");
                foreach (Account account in accounts)
                {
                    Console.WriteLine($" Id аккаунта: {account.AccountId}. Баланс: {account.Amount}");
                }
            }

            return dictionary;
        }

        // генерация нового аккаунта
        public static Account GenerateNewAccount()
        {
            // создаем сам аккаунт
            Account account = new Account()
            {

                AccountId = random.Next(100, 9999),
                Currency = GenerateCurrency(),
                Amount = 0, // random.Next(0, 99999)
            };

            return account;

        }
        public static Currency GenerateCurrency()
        {
            Currency currency = new Currency()
            {
                Name = "USD",
                ExchangeRate = 16.3,
            };

            return currency;
        }

        public static void ViewDataClientAccounts(Dictionary<Client, List<Account>> Data, Client client)
        {
            if (ClientService.DoesClientHaveAccounts(Data, client))
            {
                // получаем существующий список аккаунтов клиента
                List<Account> accountList = Data[client];

                Console.WriteLine($"Аккаунты клиента {client.FirstName}:");
                foreach (Account account in accountList)
                {
                    Console.WriteLine("Старт");
                    Console.WriteLine($"Id аккаунта: {account.AccountId}. Баланс: {account.Amount}");
                }
            }
            else 
            {
                Console.WriteLine("Данных нет");
            }            
        }
    }
}
