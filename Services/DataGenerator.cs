using Bogus;
using Models;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Services
{
    public class DataGenerator
    {
        static Random random = new Random();

        //а) генерации коллекции клиентов банка;
        public static void GenerationClients(int count)
        {
            ClientService clientService = new ClientService();
            Client client = new Client();

            for (int i = 0; i < count; i++)
            {
                var faker = new Faker<Client>()
                    .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                    .RuleFor(c => c.LastName, f => f.Person.LastName)
                    .RuleFor(c => c.DateOfBirth, f => f.Date.Past(30))
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(c => c.PassportData, f => "I" + f.Random.Number(100000, 999999))
                    .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                    .RuleFor(c => c.PhoneNumber, f => f.Person.Phone);

                client = faker.Generate();

                clientService.AddClient(client);
            }
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
        public static void GenerationEmployees(int count)
        {
            EmployeeService employeeService = new EmployeeService();

            for (int i = 0; i < count; i++)
            {
                Employee employee = new Employee();

                var faker = new Faker<Employee>()
                    .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                    .RuleFor(c => c.LastName, f => f.Person.LastName)
                    .RuleFor(c => c.DateOfBirth, f => f.Date.Past(30))
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.PassportData, f => "I" + f.Random.Number(100000, 999999))
                    .RuleFor(e => e.Department, f => f.Commerce.Department())
                    .RuleFor(e => e.Salary, f => f.Random.Number(5, 70)*100)
                    .RuleFor(e => e.Contract, f => "N" + f.Random.Number(100, 9999));
                
                employee = faker.Generate();

                employeeService.AddEmployee(employee);
            }
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

            foreach (Employee employee in Employees)
            {
                if (employee.Salary < minSalary)
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
                
        public static Currency GenerateCurrency()
        {
            Currency currency = new Currency()
            {
                name = "USD",
                ExchangeRate = 16.3,
            };

            return currency;
        }        
    }
}
