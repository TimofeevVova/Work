using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExportTool;
using Helpers;
using Models;
using Services;

namespace ExportTool
{
    public class ThreadAndTaskTests
    {       
        //а) алгоритм импорта клиентов в банковскую систему (применяем функционал ExportService) 
        //и параллельный экспорт клиентов из базы в файл (применяем 2 разных файла);
        public static void ExportAndInput(List<Client> clients)
        {
            string pathToDirectory = "D:\\Work\\Work\\test";
            string textFileName = "allDB.csv";
            ExportService exportService = new ExportService(pathToDirectory, textFileName);

            // создание мьютекса
            Mutex mutex = new();

            Thread myThread = new(FileToBd);
            myThread.Start();
            void FileToBd()
            {
                mutex.WaitOne();
                // из файла в БД
                exportService.FromFileToDB("NewData.csv");
                mutex.ReleaseMutex();
            }

            Console.WriteLine("Good");

            Thread meThread2 = new(BdToFile);
            meThread2.Start();
            void BdToFile()
            {
                mutex.WaitOne();
                // из БД в файл
                exportService.SaveClientToFile(clients);
                mutex.ReleaseMutex();
            }
        }

        //б) реализовать параллельное начисление денег на один и тот же тестовый счет (экземпляр класса Account, хранимый в памяти) 
        //из двух разных потоков  (каждый поток начисляет в цикле 10 раз по 100$), 
        //оценить равенство результирующей суммы ожидаемому результату;
        public static void TestAccruals(int clientId, int count)
        {
            // подключение и начисление аккаунту с Id clientId баланс count
            ClientService clientService = new ClientService();
            clientService.AddBalanse(clientId, count);

            int rowCount = 10;


            Task task1 = Task.Run(() =>
            {
                using (var context = new ApplicationContext())
                {
                    var service = new ClientService(context);
                    for (int i = 0; i < rowCount; i++)
                    {
                        service.AddBalanse(clientId, 100);
                        Console.WriteLine("1");
                    }
                }
            });

            Task task2 = Task.Run(() =>
            {
                using (var context = new ApplicationContext())
                {
                    var service = new ClientService(context);
                    for (int i = 0; i < rowCount; i++)
                    {
                        service.AddBalanse(clientId, 100);
                        Console.WriteLine("2");
                    }
                }
            });

            // Ждем завершения обеих задач
            Task.WaitAll(task1, task2);
        }
    }
}
