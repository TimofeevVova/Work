using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using Services.Exceptions;
using NUnit.Framework;
using System.Net;
using Services.Storage;
using HelloApp;
using static HelloApp.ApplicationContext;

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


        public client GetClient(int clientId)
        {
            return _dbContext.clientData.FirstOrDefault(c => c.Id == clientId);
        }
        public void AddClient(client client)
        {
            _dbContext.clientData.Add(client);
            _dbContext.SaveChanges();
        }
        public void RemoveClient(client client)
        {

        }
    }
}
