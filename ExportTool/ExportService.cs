using CsvHelper;
using CsvHelper.Configuration;
using Models;
using Newtonsoft.Json;
using Services;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ExportTool
{
    public class ExportService
    {
        private string _pathToDirectory { get; set; }
        private string _csvFileName { get; set; }

        public ExportService(string pathToDirectory = null, string csvFileName= null)
        {
            _pathToDirectory = pathToDirectory;
            _csvFileName = csvFileName;
        }

        ClientService clientService = new ClientService();
        EmployeeService employeeService = new EmployeeService();

        // из БД в файл
        public void SaveClientToFile(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirectory);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            using (var writer = new StreamWriter(fullPath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            }))
            {
                csv.WriteRecords(clients);
            }
            Console.WriteLine("Данные успешно записаны в CSV файл");
        }

        // из файла в БД
        public void FromFileToDB(string FileName)
        {
            

            string fullPath = Path.Combine(_pathToDirectory, FileName);

            using (var reader = new StreamReader(fullPath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" }))
            {
                var clients = csv.GetRecords<Client>().ToList();

                foreach (var client in clients)
                {
                    client.ClientId = 0;
                    clientService.AddClient(client);
                }
            }
            Console.WriteLine("Данные успешно добавлены в базу из CSV файла");
        }


        // Применяем JSON сериализацию.Кроме прочего, в методы передается путь к файлу.
        // методы для экспорта клиента в сериализованном виде в файл(.txt или .json)
        public void ExportObjectInFile<T>(T obj, string filePath)
        {
            // Проверяем, что переданный объект не null
            if (obj != null)
            {
                // Сериализуем объект в JSON
                string json = JsonConvert.SerializeObject(obj);

                // Записываем в файл
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(json);
                    Console.WriteLine("Данные успешно записаны в файл.");
                }
            }
            else
            {
                Console.WriteLine("Объект для экспорта равен null.");
            }
        }

        // и метод импорта клиента из сериализованной записи в файле
        public void ImportObjectFromFile<T>(string filePath)
        {
            try
            {
                // Читаем JSON из файла
                string json = File.ReadAllText(filePath);

                // Десериализуем JSON в объект
                T obj = JsonConvert.DeserializeObject<T>(json);

                Console.WriteLine("Данные успешно записаны в БД из файла.");

                ProcessImportedObject(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении данных из файла: {ex.Message}");
            }
        }

        // запись объекта в БД
        private void ProcessImportedObject(object obj)
        {
            if (obj is Client)
            {
                clientService.AddClient((Client)obj);
            }
            else if (obj is Employee)
            {
                //employeeService.ShowEmployeeData((Employee)obj);
                employeeService.AddEmployee((Employee)obj);
            }
            else
            {
                Console.WriteLine("Неизвестный тип объекта.");
            }
        }
    }
}
