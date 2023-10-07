using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HelloApp
{
    public class ApplicationContext : DbContext // определяет контекст данных, используемый для взаимодействия с базой данных;
    {
        public DbSet<Client> clientData { get; set; }  // представляет набор объектов Клиент, которые хранятся в базе данных;
        public DbSet<Account> accountData { get; set; }  // представляет набор объектов Аккаунт, которые хранятся в базе данных;
        public DbSet<Employee> employeeData { get; set; }  // представляет набор объектов Сотрудник, которые хранятся в базе данных;

        public ApplicationContext()
        {
            Console.WriteLine("Зашли в ApplicationContext");
            Database.EnsureCreated();
            Console.WriteLine("Получили ответ");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // устанавливает параметры подключения.
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
            Console.WriteLine("Запрос к БД");
        }
    }
}
