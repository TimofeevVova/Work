using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using Models;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace MainProgram
{
    internal class Dispose
    {
        private IConfiguration Configuration { get; }
        private ApplicationContext _dbContext; // поле для хранения контекста данных

        public Dispose(ApplicationContext dbContext) // Принимаем контекст данных через конструктор
        {
            _dbContext = dbContext;

            // инициализация конфигурации
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // открытие подключения
        private void OpenConnection()
        {
            //_dbContext.Database.OpenConnection();
            // LogOpenedConnectionCount(); // Записываем число открытых подключений

            using (var connection = _dbContext.Database.GetDbConnection())
            {
                LogOpenedConnectionCount();
                // Ваш код работы с открытым соединением
            }
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
            for (var i = 0; i < 200; i++)
            {
                OpenConnection();
            }
        }
    }
}
