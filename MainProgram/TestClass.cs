using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using Models; // Добавлен импорт пространства имен с вашим контекстом данных
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace MainProgram
{
    internal class TestClass
    {
        private IConfiguration Configuration { get; }
        private ApplicationContext _dbContext; // Добавили поле для хранения контекста данных

        public TestClass(ApplicationContext dbContext) // Принимаем контекст данных через конструктор
        {
            _dbContext = dbContext;

            // Инициализация конфигурации
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // Откроем подключение
        private void OpenConnection()
        {
            // Используем контекст данных
            _dbContext.Database.OpenConnection();
            LogOpenedConnectionCount(); // Записываем число открытых подключений

        }

        private void LogOpenedConnectionCount()
        {
            _connectionsCounter++;
            Console.WriteLine(_connectionsCounter);
        }

        private int _connectionsCounter = 0;

        // Сделаем попытку открыть 200 подключений к postgres:
        public void StartOpenConnections()
        {
            for (var i = 0; i < 900000; i++)
            {
                OpenConnection();
            }
        }
    }
}
