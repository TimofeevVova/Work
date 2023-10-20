using ExportTool;
using Services;

namespace Helpers
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            ClientService clientService = new ClientService();
            EmployeeService employeeService = new EmployeeService();
            ExportService exportService = new ExportService();
            CashDispenserService cashDispenserService = new CashDispenserService(10);
            CurrencyService currencyService = new CurrencyService();

            Console.WriteLine("Start program");

            //добавить в БД 
            //DataGenerator.GenerationClients(5);            
            //DataGenerator.GenerationEmployees(5);


            Console.ReadKey();
        }
    }
}
