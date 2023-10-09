using CsvHelper;
using CsvHelper.Configuration;
using Models;
using Services;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ExportTool
{
    public class ExportService
    {
        private string _pathToDirectory { get; set; }
        private string _csvFileName { get; set; }

        public ExportService(string pathToDirectory, string csvFileName)
        {
            _pathToDirectory = pathToDirectory;
            _csvFileName = csvFileName;
        }

        // из БД в файл
        public void SaveClientToFile(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirectory);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            using (var writer = new StreamWriter(fullPath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(clients);
            }
            Console.WriteLine("Данные успешно записаны в CSV файл");
        }

        // из файла в БД
        public void FromFileToDB()
        {
            ClientService clientService = new ClientService();

            string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

            using (var reader = new StreamReader(fullPath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" }))
            {
                var clients = csv.GetRecords<Client>().ToList();

                foreach (var client in clients)
                {
                    clientService.AddClient(client);
                }
            }
            Console.WriteLine("Данные успешно добавлены в базу из CSV файла");
        }
    }
}
