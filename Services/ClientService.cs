using Helpers;
using Models;

namespace Services
{
    public class ClientService
    {
        ApplicationContext _dbContext;
        public ClientService()
        {
            _dbContext = new ApplicationContext();
        }

        public ClientService(ApplicationContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // показать данные клиента
        public void ShowClientData (Client client)
        {
            if (client != null)
            {
                Console.WriteLine($"ID: {client.ClientId} " +
                    $"Имя: {client.FirstName}, " +
                    $"Фамилия: {client.LastName} " +
                    $"Адрес: {client.Address}, " +
                    $"ДатаРожд: {client.DateOfBirth.ToString("yyyy-MM-dd")}, " +
                    $"Email: {client.Email}, " +
                    $"Phone: {client.PhoneNumber}");
            }
            else { Console.WriteLine("Такого Id нет в базе данных"); }
        }

        // показать данные списка клиентов
        public void ShowSomeClientsData(List<Client> clients)
        {
            foreach (Client client in clients) 
            {
                ShowClientData(client);
            }            
        }

        //а) получить клиента по идентификатору;
        public Client GetClient(int clientId)
        {
            return _dbContext.clientData.FirstOrDefault(c => c.ClientId == clientId);
        }

        public List<Client> GetAllClients(int page = 1, int pageSize = int.MaxValue)
        {
            int skipAmount = (page - 1) * pageSize;

            var clients = _dbContext.clientData.Skip(skipAmount).Take(pageSize).ToList();

            return clients;
        }

        //б) добавить нового клиента(автоматически создает дефолтный лицевой счет);
        public void AddClient(Client client)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    client.ClientId = 0;
                    _dbContext.clientData.Add(client);
                    _dbContext.SaveChanges();

                    Account account = new Account
                    {
                        Currency = "USD",
                        Amount = 0,
                        OwnerId = client.ClientId
                    };

                    _dbContext.accountData.Add(account);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        //в) добавить клиенту новый лицевой счет(принимает на вход Id клиента);
        public void AddAccount(int clientId)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Client client = _dbContext.clientData.FirstOrDefault(c => c.ClientId == clientId);

                    Account account = new Account
                    {
                        Currency = "USD",
                        Amount = 0,
                        OwnerId = client.ClientId
                    };

                    _dbContext.accountData.Add(account);
                    _dbContext.SaveChanges();

                    transaction.Commit(); 
                }
                catch (Exception)
                {
                    transaction.Rollback(); 
                    throw;
                }
            }
        }

        //г) изменить клиента по идентификатору;
        public void UpdateClient(int clientId, Client client)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {                    
                    Client existingClient = _dbContext.clientData.Find(clientId);

                    if (existingClient != null)
                    {
                        //existingClient.ClientId = Client.ClientId != 0 ? Client.ClientId : existingClient.ClientId;
                        existingClient.FirstName = client.FirstName != null ? client.FirstName : existingClient.FirstName;
                        existingClient.LastName = client.LastName != null ? client.LastName : existingClient.LastName;
                        existingClient.DateOfBirth = client.DateOfBirth != default(DateTime) ? client.DateOfBirth : existingClient.DateOfBirth;
                        existingClient.Address = client.Address != null ? client.Address : existingClient.Address;
                        existingClient.PassportData = client.PassportData != null ? client.PassportData : existingClient.PassportData;
                        existingClient.Email = client.Email != null ? client.Email : existingClient.Email;
                        existingClient.PhoneNumber = client.PhoneNumber != null ? client.PhoneNumber : existingClient.PhoneNumber;

                        _dbContext.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        //д) удалить клиента по идентификатору.
        public void RemoveClient(int clientId)
        {
            Client client = _dbContext.clientData.FirstOrDefault(c => c.ClientId == clientId);

            if (client != null)
            {
                _dbContext.clientData.Remove(client);
                _dbContext.SaveChanges();
            }
        }

        //е) удалить лицевой счет клиента;
        public void RemoveAccount(int accountId)
        {
            Account account = _dbContext.accountData.FirstOrDefault(c => c.AccountId == accountId);

            if (account != null)
            {
                _dbContext.accountData.Remove(account);
                _dbContext.SaveChanges();
            }
            else { Console.WriteLine("Такого аккаунта нет в базе данных"); }
        }

        //ж) метод возвращающий список клиентов, удовлетворяющих фильтру(+ пагинация)
        //(протестировать на операторах Where, OrderBy, GroupBy, Take, посмотреть sql, в логах(консоли), оценить отличие от Linq;
        //з) в рамках пункта “ж”, в режиме отладки, проследить в какой момент времени формируемый из сегментов фильтра зарос, выполняется.

        public List<Client> GetFilteredClients(Func<Client, bool> filter, Func<Client, object> orderBy, int pageNumber, int pageSize)
        {
            var query = _dbContext.clientData.Where(filter);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            // Применение пагинации
            var paginatedResult = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            // вывод данных в консоль
            ShowSomeClientsData(paginatedResult);

            return paginatedResult;
        }

        // получить баланс клиента
        public double GetBalanse(int accountId)
        {
            Account account = _dbContext.accountData.FirstOrDefault(c => c.AccountId == accountId);
            if(account != null)
            {
                return account.Amount;
            }
            return 0;            
        } 

        // добавление баланса на счет
        public void AddBalanse(int accountId, int count)
        {
            Account account = _dbContext.accountData.FirstOrDefault(c => c.AccountId == accountId);

            if (account != null)
            {
                if (account.Amount > 0)
                {
                    account.Amount += count;
                    _dbContext.SaveChanges();
                    Console.WriteLine($"В аккаунт: {accountId} начислено: {count}");
                }
                else
                {
                    Console.WriteLine($"Неверное количество");
                }
            }
            else
            {
                Console.WriteLine("Аккаунт не найден");
            }
        }

        // списание баланса со счета
        public void SubtractBalance(int accountId, double count)
        {
            Account account = _dbContext.accountData.FirstOrDefault(c => c.AccountId == accountId);

            if (account != null)
            {
                if (account.Amount > 0)
                {
                    account.Amount -= count;
                    _dbContext.SaveChanges();
                    Console.WriteLine($"В аккаунте: {accountId} списано: {count} единиц денег");
                }
                else
                {
                    Console.WriteLine($"В аккаунте: {accountId} недостаточно средств");
                }                
            }
            else
            {
                Console.WriteLine("Аккаунт не найден");
            }
        }
    }
}
