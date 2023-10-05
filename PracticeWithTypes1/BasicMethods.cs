using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWithTypes
{
    public class BasicMethods
    {
        // Создание и отобравжение классов и структуры
        public static void FillingAndDisplaying()
        {
            Person vanya = new Person // обычное заполение данных человенка
            {
                FirstName = "Иван",
                LastName = "Иванов",
                DateOfBirth = new DateTime(2000, 3, 15),
                Address = "Тирасполь "
            };
            Employee petya = new Employee // заполнение данных человека и данных сотрудника
            {
                FirstName = "Петр",
                LastName = "Петров",
                DateOfBirth = new DateTime(1982, 7, 20),
                Address = "Рыбница",

                EmployeeId = 1,
                Department = "IT",
                Salary = 5000
            };
            Client anna = new Client // заполнение данных человека и данных клиента
            {
                FirstName = "Анна",
                LastName = "Сидорова",
                DateOfBirth = new DateTime(1989, 2, 8),
                Address = "Днестровск",

                ClientId = 4,
                Email = "anna@example.com",
                PhoneNumber = 77900000
            };
            Currency usd = new Currency // заполнение валюты
            {
                Name = "USD",
                ExchangeRate = 16.3
            };

            Console.WriteLine($"Person: {vanya.FirstName} {vanya.LastName}, Дата рождения: {vanya.DateOfBirth}");
            Console.WriteLine($"Employee: {petya.FirstName} {petya.LastName}, ID сотрудника: {petya.EmployeeId}, Зарплата: {petya.Salary}");
            Console.WriteLine($"Client: {anna.FirstName} {anna.LastName}, ID клиента: {anna.ClientId}, Email: {anna.Email}, Телефон: {anna.PhoneNumber}");
            Console.WriteLine($"Валюта: {usd.Name}, Обменный курс: {usd.ExchangeRate}");

        }

        // Изменение данных
        public static void ChangeExchangeRate()
        {
            Employee employee = new Employee
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Department = "IT",
                Salary = 5000,
                Contract = ""
            };

            Currency currency = new Currency
            {
                Name = "Доллар",
                ExchangeRate = 1.0
            };
            Console.WriteLine("\n");
            Console.WriteLine($"Исходный контракт сотрудника: {employee.Contract}");
            Console.WriteLine($"Исходный курс обмена валюты: {currency.ExchangeRate}");


            //  метод для обновления контракта сотрудника.
            ContractUpdater.UpdateEmployeeContract(employee);

            // метод для обновления курса обмена валюты.
            ContractUpdater.UpdateCurrency(ref currency, 17.2);

            Console.WriteLine("\n");
            Console.WriteLine($"Обновленный контракт сотрудника: {employee.Contract}");
            Console.WriteLine($"Обновленный курс обмена валюты: {currency.ExchangeRate}");
        }

        // Расчет зарплаты директоров
        public static void SalaryCalculation()
        {
            List<Employee> employees = new List<Employee>();

            Employee admin1 = new Employee()
            {
                FirstName = "Игорь",
                LastName = "Сергеевич",
                DateOfBirth = new DateTime(1992, 7, 20),
                Address = "Тирасполь",

                EmployeeId = 1001,
                Department = "Директор",
            };
            employees.Add(admin1);

            Employee admin2 = new Employee()
            {
                FirstName = "Игорь",
                LastName = "",
                DateOfBirth = new DateTime(1995, 3, 22),
                Address = "Сейшелы",

                EmployeeId = 1002,
                Department = "Директор",
            };
            employees.Add(admin2);

            Employee admin3 = new Employee()
            {
                FirstName = "Игорь",
                LastName = "",
                DateOfBirth = new DateTime(1988, 11, 3),
                Address = "Слободзея",

                EmployeeId = 1003,
                Department = "Директор",
            };
            employees.Add(admin3);


            int profit = 65000; // прибыль
            int expenses = 7500; // затраты


            int salary = BankService.SalaryCalculation(profit, expenses, employees);
            Console.WriteLine("\n");
            Console.WriteLine("salary: ");
            Console.WriteLine(salary);
        }

        // преобразования клиента банка в сотрудника
        public static void ConvertClientToEmployee()
        {
            Client client = new Client()
            {
                FirstName = "Виталий",
                LastName = "Новиков",
                DateOfBirth = new DateTime(1998, 10, 3),
                Address = "Суклея",

                ClientId = 1,
                Email = "Client@example.com",
                PhoneNumber = 1234567890
            };

            Employee employee2 = BankService.ConvertClientToEmployee(client);
        }

        // Учет времени упаковки и распаковки
        public static void TimeTracking()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            int i = 42;
            object obj = i; // упаковка
            stopwatch.Stop();
            long PackingTime = stopwatch.ElapsedTicks;

            Console.WriteLine("\n");
            Console.WriteLine($"Время упаковки - {PackingTime} тиков");

            stopwatch.Reset();
            stopwatch.Start();
            int j = (int)obj; // Распаковка
            stopwatch.Stop();
            long UnpackingЕime = stopwatch.ElapsedTicks;

            Console.WriteLine($"Время распаковки - {UnpackingЕime} тиков");
            Console.WriteLine("\n");
        }
    }
}
