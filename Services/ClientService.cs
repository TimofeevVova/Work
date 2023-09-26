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

namespace Services
{
    public class ClientService // работа со счетами клиентов
    {
        private readonly ClientStorage clientStorage;
        public ClientService(ClientStorage clientStorage)
        {
            this.clientStorage = clientStorage;
            Data = new Dictionary<Client, List<Account>>();
        }
        public Dictionary<Client, List<Account>> Data { get; set; } = new Dictionary<Client, List<Account>>();


        // добавить новый аккаунт клиенту
        public void AddAccount(Client client)
        {
            // создание нового счета
            Account account = TestDataGenerator.GenerateNewAccount();

            // установка связи аккаунта и клиента
            // запись id клиента в аккаунт
            account.AccountId = client.ClientId;
            // запись id аккаунта в данные клиента
            client.IdAccounts.Add(account.AccountId);

            // присвоение счета к клиенту
            Data[client].Add(account);            
            Console.WriteLine("Создан счет для клиента");
        }



        // Обновить данные в аккаунте
        public void UpdateAccount(Client client, int newAmount)
        {
            // проверяем наличие клиента и есть ли у него счет
            if (DoesClientHaveAccounts(Data, client))
            {
                if (Data.ContainsKey(client))
                {
                    // получаем существующий список аккаунтов клиента
                    List<Account> accountList = Data[client];

                    int id = accountList[0].AccountId;
                    if (id != 0)
                    {
                        Console.WriteLine($"ID - {id}, Баланс -  {accountList[0].Amount}");
                        // замена счета на новый
                        accountList[0].Amount = newAmount;
                        Console.WriteLine("Изменено на:");
                        Console.WriteLine($"ID - {id}, Баланс -  {accountList[0].Amount}");
                    }
                    else
                    {
                        Console.WriteLine("Аккаунт не найден");
                    }
                }
                else
                {
                    Console.WriteLine("Данного клиента нет");
                }
            }
            else
            {
                Console.WriteLine("Двнного клиента или счета нет");
            }            
        }


        // Удалить аккаунт
        public void DeleteAccount(Client client) 
        {
            Data.Remove(client);
        }

        // проверка на наличие аккаунта у клиента
        public static bool DoesClientHaveAccounts(Dictionary<Client, List<Account>> Data, Client client)
        {
            if (Data.ContainsKey(client))
            {
                // получаем список аккаунтов клиента
                List<Account> accountList = Data[client];

                // проверяем, есть ли аккаунты у клиента
                Console.WriteLine("Проверка на аккануты клиента");
                Console.WriteLine(accountList.Any());
                return accountList.Any();
            }
            else
            {
                // клиент не найден, поэтому нет и аккаунтов
                Console.WriteLine("клиент не найден, поэтому нет и аккаунтов");
                return false;
            }
        }        
    }
}
