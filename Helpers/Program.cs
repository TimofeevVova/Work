namespace Helpers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start program");

            using (var db = new ApplicationContext())
            {
                // Выводим всех клиентов из базы данных
                var allClients = db.clientData.ToList();
                Console.WriteLine("Список всех клиентов в базе данных:");

                foreach (var client in allClients)
                {
                    Console.WriteLine($"ID: {client.ClientId} " +
                        $"Имя: {client.FirstName}, " +
                        $"Фамилия: {client.LastName} " +
                        $"Адрес: {client.Address}, " +
                        $"ДатаРожд: {client.DateOfBirth.ToString("yyyy-MM-dd")}, " +
                        $"Email: {client.Email}");
                }
            }

            Console.ReadKey();
        }
    }
}
