using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class SettingsGenerator
    {
        static Random random = new Random();
        // Генерация случайной даты рождения в определенном диапазоне
        private static DateTime startDate = new DateTime(1970, 1, 1); // Начальная дата
        private static DateTime endDate = new DateTime(2005, 12, 31); // Конечная дата
        private static TimeSpan timeSpan = endDate - startDate; // Вычисляем диапазон дат





        //Настройки генератора
        public static string GetRandomFirstName()
        {
            string[] names = { "Иван", "Александр", "Михаил", "Сергей", "Дмитрий", "Андрей", "Анна", "Елена", "Мария", "Ольга", "Светлана", "Наталья", "Татьяна", "Екатерина", "Ирина", "Людмила", "Виктория", "Оксана", "Надежда", "Лариса", "Тамара", "Юлия", "Галина", "Алла", "Валентина", "Раиса", "Лидия", "Зинаида", "Анатолий", "Владимир", "Павел", "Николай", "Константин", "Юрий", "Олег", "Антон", "Артем", "Лев", "Илья", "Фёдор", "Даниил", "Семён", "Петр", "Степан", "Алексей", "Василиса", "Роман", "Денис" };
            return names[random.Next(names.Length)];
        }

        public static string GetRandomName()
        {
            string[] names = { "Иванов(а)", "Смирнов(а)", "Кузнецов(а)", "Попов(а)", "Васильев(а)", "Петров(а)", "Соколов(а)", "Михайлов(а)", "Новиков(а)", "Федоров(а)", "Морозов(а)", "Волков(а)", "Алексеев(а)", "Лебедев(а)", "Семенов(а)", "Егоров(а)", "Павлов(а)", "Козлов(а)", "Степанов(а)", "Николаев(а)", "Орлов(а)", "Андреев(а)", "Макаров(а)", "Никитин(а)", "Захаров(а)", "Зайцев(а)", "Соловьев(а)", "Борисов(а)", "Яковлев(а)", "Григорьев(а)", "Романов(а)", "Воробьев(а)", "Сергеев(а)", "Кузьмин(а)", "Фролов(а)", "Александров(а)", "Дмитриев(а)", "Королев(а)", "Гусев(а)", "Киселев(а)", "Ильин(а)", "Максимов(а)", "Поляков(а)", "Сорокин(а)", "Серебряков(а)", "Кудрявцев(а)", "Игнатьев(а)", "Литвинов(а)" };
            return names[random.Next(names.Length)];
        }

        public static DateTime GetRandomDateOfBirth()
        {
            int randomDays = random.Next(0, (int)timeSpan.TotalDays); // Генерируем случайное количество дней
            DateTime randomDateOfBirth = startDate.AddDays(randomDays); // Добавляем случайное количество дней к начальной дате
            return randomDateOfBirth;
        }

        public static string GetRandomAddress()
        {
            string[] addresses = { "Тирасполь", "Бендеры", "Рыбница", "Дубоссары", "Слободзея", "Григориополь", "Каменка", "Днестровск", "Заставна", "Захаро-Бена", "Корнешты", "Красное", "Слободское", "Страшены", "Шевченко" };
            return addresses[random.Next(addresses.Length)];
        }

        public static string GetRandomEmail()
        {
            string[] domains = { "gmail.com", "yahoo.com", "mail.com", "hotmail.com", "bing.com" };
            string[] firstNames = { "Alexandr", "Petya", "Ivan", "Igor", "Anna" };
            string[] lastNames = { "Smirnov", "Petro", "Volk", "Znoy", "Zayas" };

            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];
            string domain = domains[random.Next(domains.Length)];
            int value = random.Next(1, 999);

            return $"{firstName}_{lastName}{value}@{domain}";
        }

        public static string GetRandomPhoneNumber()
        {
            int pre = random.Next(5, 9);
            int rest = random.Next(00000, 99999);
            return $"0-77{pre}-{rest}";
        }

        public static string GetRandomDepartment()
        {
            string[] Departments = { "Отдел кадров", "Директор", "Обслуживание", "Разработчик", "Логист" };
            string Department = Departments[random.Next(Departments.Length)];

            return $"{Department}";
        }

        public static int GetRandomSalary()
        {
            return random.Next(5, 20) * 100;
        }

        public static string GetRandomContract()
        {
            int number = random.Next(200, 1000);
            return $"{number}";
        }

    }
}
