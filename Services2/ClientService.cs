using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Services.Storage;
using BankContext;

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


        //а) получить клиента по идентификатору;
        public Client GetClient(int clientId)
        {
            return _dbContext.clientData.FirstOrDefault(c => c.ClientId == clientId);
        }


        //б) добавить нового клиента(автоматически создает дефолтный лицевой счет);
        public void AddClient(Client client)
        {
            _dbContext.clientData.Add(client);
            _dbContext.SaveChanges();
        }


        //в) добавить клиенту новый лицевой счет(принимает на вход Id клиента);
        public void AddAccountFromClient(int clientId)
        {

        }


        //г) изменить клиента по идентификатору;
        public void ChangeClientData(int clientId)
        {

        }


        //д) удалить клиента по идентификатору.
        public void RemoveClient(int clientId)
        {

        }
        //е) удалить лицевой счет клиента;
        public void RemoveClientAccount(int accountId)
        {

        }


        //ж) метод возвращающий список клиентов, удовлетворяющих фильтру(+ пагинация) (протестировать на операторах Where, OrderBy, GroupBy, Take, посмотреть sql, в логах(консоли), оценить отличие от Linq;
        //з) в рамках пункта “ж”, в режиме отладки, проследить в какой момент времени формируемый из сегментов фильтра зарос, выполняется.
        public List<Client> GetFilterClients(string filter) 
        {
            List<Client> clients = new List<Client>();


            return clients;
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
    }
}
