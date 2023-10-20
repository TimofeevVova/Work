using Models;
using Services.Exceptions;

namespace Services.Storage
{
    public class ClientStorage
    {
        Random random = new Random();
        //в хранилище добавить readonly список для хранение клиентов банка, 
        public readonly List<Client> clients = new List<Client>();

        public Dictionary<Client, List<Account>> Data { set; get; } = new Dictionary<Client, List<Account>>();
        //Добавить нового клиента
        public Client Add(string firstName, string lastName, DateTime dateOfBirth, string addres, string email, string phoneNumber, string passportData = "")
        {
            DateTime today = DateTime.Today;
            DateTime minAge = today.AddYears(-18);
            Client newClient = new Client();

            if (passportData != "")
            {
                newClient.FirstName = firstName;
                newClient.LastName = lastName;
                newClient.DateOfBirth = dateOfBirth;
                newClient.Address = addres;
                newClient.PassportData = passportData;
                newClient.Email = email;
                newClient.PhoneNumber = phoneNumber;
                newClient.ClientId = random.Next(100, 9999);

                clients.Add(newClient);

                List<Account> emptyAccountList = new List<Account>();
                Data.Add(newClient, emptyAccountList);


                Console.WriteLine("\n");
                Console.WriteLine("Новое добавление");
                Console.WriteLine($"Имя- {newClient.FirstName} " +
                    $"Город- {newClient.Address} " +
                    $"Возраст- {newClient.DateOfBirth:dd/MM/yyyy} " +
                    $"Телефон- {newClient.PhoneNumber} " +
                    $"Id-{newClient.ClientId}");

                return newClient;
            }
            else
            {
                // выбрасываем исключение если у клиента нет паспортных данных;
                throw new ExceptionNoPassportData();
            }
        }
       
        public IEnumerable<Client> GetClients(Func<Client, bool> filter)
        {
            return clients.Where(filter);
        }
    }
}
