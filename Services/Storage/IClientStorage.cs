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
        void AddAccount(Client client, Account account);
        //UpdateAccount
        void UpdateAccount(Client client, Account account);
        //DeleteAccount
        void DeleteAccount(Client client, Account account);


        //свойство Dictionary<Client, List<Account>> Data.
        Dictionary<Client, List<Account>> Data { get; set; }

    }
}
