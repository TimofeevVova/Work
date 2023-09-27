using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Storage
{
    public interface IClientStorage : IStorage
    {
        //AddAccount
        Dictionary<Client, List<Account>> AddAccount(Client client);
        //UpdateAccount
        Dictionary<Client, List<Account>> UpdateAccount(Client client, int idAccount, int newAmount);
        //DeleteAccount
        Client DeleteAccount(Client client, Account account);


        //свойство Dictionary<Client, List<Account>> Data.
        Dictionary<Client, List<Account>> Data { get; set; }

    }
}
