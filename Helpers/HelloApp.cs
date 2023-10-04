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
       
        public DbSet<client> clientData { get; set; }  // представляет набор объектов, которые хранятся в базе данных;

        [Table ("client")]
        public class client  // [Required]  представляет аннотацию, которая указывает, что свойство Name обязательно должно иметь значение.
        {
            [Required]
            [Column("client_id")]
            public int Id { get; set; }

            [Required]
            [Column("first_name")]
            public string Name { get; set; }

            [Required]
            [Column("last_name")]
            public string Famify { get; set; }

            [Required]
            [Column("date_of_birth", TypeName = "date")]
            public DateTime Date { get; set; }

            [Column("address")]
            public string Address { get; set; }

            [Required]
            [Column("passport_data")]
            public string Passport { get; set; }

            [Required]
            [Column("email")]
            public string Email { get; set; }

            [Column("phone_number")]
            public int Phone { get; set; }
        }

        public ApplicationContext()
        {
            Console.WriteLine("Зашли в ApplicationContext");

            Database.EnsureCreated();

            Console.WriteLine("Получили ответ");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // устанавливает параметры подключения.
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PostgreSQL16;Username=postgres;Password=postgres");

            Console.WriteLine("Запрос к БД");
        }
    }
}
