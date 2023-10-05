using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Services.Storage;
using HelloApp;

namespace Services
{
    public class ClientService 
    {
        private readonly IClientStorage storage;
        ApplicationContext _dbContext;
        public ClientService(IClientStorage storage)
        {
            this.storage = storage;
            _dbContext = new ApplicationContext();
        }

        // проверка на наличие аккаунта у клиента
        public static bool DoesClientHaveAccounts(Dictionary<Client, List<Account>> Data, Client client, int idAccount)
        {
            if (Data.ContainsKey(client))
            {
                List<Account> accountList = Data[client];               
                bool hasAccount = accountList.Any(account => account.AccountId == idAccount);
                return hasAccount;
            }
            else
            {                
                return false;
            }
        }


        public Client GetClient(int clientId)
        {
            return _dbContext.clientData.FirstOrDefault(c => c.Id == clientId);
        }
        public void AddClient(Client client)
        {
            _dbContext.clientData.Add(client);
            _dbContext.SaveChanges();
        }
        public void RemoveClient(Client client)
        {

        }
    }
}
