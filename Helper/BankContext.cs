using Microsoft.EntityFrameworkCore;
using Models;

namespace BankContext
{
    public class ApplicationContext : DbContext // определяет контекст данных, используемый для взаимодействия с базой данных;
    {
        public DbSet<Client> clientData { get; set; }  // представляет набор объектов, которые хранятся в базе данных;
        public DbSet<Account> accountData { get; set; }
        public DbSet<Employee> employeeData { get; set; }

        public ApplicationContext()
        {
            Console.WriteLine("Зашли в ApplicationContext");
            Database.Migrate();
            Console.WriteLine("Получили ответ");
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // устанавливает параметры подключения.
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
            Console.WriteLine("Запрос к БД");
        }
    }
}
