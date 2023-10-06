using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services.Exceptions;

namespace Services.Storage
{
    public class ClientStorage : IClientStorage
    {
        Random random = new Random();
        //в хранилище добавить readonly список для хранение клиентов банка, 
        public readonly List<Client> clients = new List<Client>();

        public Dictionary<Client, List<Account>> Data { set; get; } = new Dictionary<Client, List<Account>>();
        //Добавить нового клиента
        public Client Add(string FirstName, string LastName, DateTime DateOfBirth, string Addres, string Email, string PhoneNumber, string passportData = "")
        {
            DateTime today = DateTime.Today;
            DateTime MinAge = today.AddYears(-18);
            Client newClient = new Client();
            try
            { 
                if (MinAge > DateOfBirth)
                {
                    if (passportData != "")
                    {
                        newClient.FirstName = FirstName;
                        newClient.LastName = LastName;
                        newClient.DateOfBirth = DateOfBirth;
                        newClient.Address = Addres;
                        newClient.SetPasportData(passportData);
                        newClient.Email = Email;
                        newClient.PhoneNumber = PhoneNumber;
                        newClient.ClientId = random.Next(100, 9999);

                        clients.Add(newClient);

                        List<Account> emptyAccountList = new List<Account>();                        
                        Data.Add(newClient, emptyAccountList);


                        Console.WriteLine("\n");
                        Console.WriteLine("Новое добавление");
                        Console.WriteLine($"Имя- {newClient.FirstName} Город- {newClient.Address} Возраст- {newClient.DateOfBirth:dd/MM/yyyy} Телефон- {newClient.PhoneNumber} Id-{newClient.ClientId}");
                        return newClient;
                    }
                    else
                    {
                        // выбрасываем исключение если у клиента нет паспортных данных;
                        throw new ExceptionNoPassportData();
                    }
                }
                else
                {
                    // выбрасываем исключение если клиент моложе 18 лет
                    throw new ExceptionAge();
                }
            }
            catch (ExceptionAge e)
            {
                Console.WriteLine(e.ToString());
                //return Data;
            }
            catch (ExceptionNoPassportData e)
            {
                Console.WriteLine(e.ToString());
                //return Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //return Data;
            }
            return newClient;
        }
        // редактирование клиента
        public Client Update(Client client, string FirstName, string LastName, DateTime DateOfBirth, string Address, string Email, string PhoneNumber, string passportData = "")
        {            
            // индекс клиента в списке
            int index = clients.IndexOf(client);

            if (index >= 0)
            {
                try
                {
                    DateTime today = DateTime.Today;
                    DateTime MinAge = today.AddYears(-18);

                    if (MinAge > DateOfBirth)
                    {
                        if (passportData != "")
                        {
                            clients[index].FirstName = FirstName;
                            clients[index].FirstName = FirstName;
                            clients[index].LastName = LastName;
                            clients[index].DateOfBirth = DateOfBirth;
                            clients[index].Address = Address;
                            clients[index].Email = Email;
                            clients[index].PhoneNumber = PhoneNumber;

                            clients[index].SetPasportData(passportData);

                            Console.WriteLine("\n");
                            Console.WriteLine("Обновление");
                            Console.WriteLine($"Имя- {clients[index].FirstName} Город- {clients[index].Address} Возраст- {clients[index].DateOfBirth:dd/MM/yyyy} Телефон- {clients[index].PhoneNumber} Id-{clients[index].ClientId}");
                            
                            return clients[index];
                        }
                        else
                        {
                            // нет паспортных данных;
                            throw new ExceptionNoPassportData();
                        }
                    }
                    else
                    {
                        // моложе 18 лет
                        throw new ExceptionAge();
                    }
                }
                catch (ExceptionAge e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (ExceptionNoPassportData e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Клиент не найден.");
                return client;
            }
            return client;
        }        
        // удалить клиента
        public void Delete(Client client)
        {
            clients.Remove(client);
            Data.Remove(client);

            Console.WriteLine("Все имена переменной Data");
            foreach (var entry in Data)
            {
                var client1 = entry.Key; // Получаем клиента из ключа
                Console.WriteLine(client1.FirstName);
            }
        }


        // добавить новый аккаунт клиенту
        public Dictionary<Client, List<Account>> AddAccount(Client client)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Начало добавления аккаунта");
            Account account = TestDataGenerator.GenerateNewAccount();            
            List<Account> accounts = new List<Account>();
            if (client.IdAccounts == null)
            {
                client.IdAccounts = new List<int>();
            }

            // для простоты поиска аккаунтов, нумерация 1,2 и т.д.
            if (client.IdAccounts.Any())
            {
                int lastAccountId = client.IdAccounts.Last();
                account.AccountId = ++lastAccountId;
            }
            else
            {
                account.AccountId += 1; 
            }

            // установка связи аккаунта и клиента
            account.OwnerId = client.ClientId;
            client.IdAccounts.Add(account.AccountId);

            if (Data.ContainsKey(client)) // клиент существует
            {                
                Data[client].Add(account);
                Console.WriteLine("Записан просто счет для клиента");
            }
            else // клиент не существует
            {                
                accounts.Add(account);
                Data.Add(client, accounts);
                Console.WriteLine("Записан новый клиент и счет для него");
            }
            Console.WriteLine("Создан счет для клиента");
            Console.WriteLine($"Имя- {client.FirstName} Город- {client.Address} Возраст- {client.DateOfBirth:dd/MM/yyyy} Телефон- {client.PhoneNumber} Id - {client.ClientId}, Аккаунты-{string.Join(", ", client.IdAccounts)}");
            Console.WriteLine($"accounts {account.AccountId}, Currency-{account.Currency}, Amount-{account.Amount}, OwnerId-{account.OwnerId}");

            return Data;
        }
        // Обновить данные в аккаунте
        public Dictionary<Client, List<Account>> UpdateAccount(Client client, int idAccount, int newAmount)
        {
            // проверяем наличие клиента и есть ли у него нужный счет
            if (ClientService.DoesClientHaveAccounts(Data, client, idAccount))
            {
                // получаем существующий список аккаунтов клиента
                List<Account> accountList = Data[client];
                Account accountToUpdate = accountList.FirstOrDefault(account => account.AccountId == idAccount);

                if (accountToUpdate != null)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine($"Изменение баланса для аккаунта с Id {accountToUpdate.AccountId}");
                    Console.WriteLine($"Текущий баланс: {accountToUpdate.Amount}");

                    // Обновляем баланс аккаунта
                    accountToUpdate.Amount = newAmount;

                    Console.WriteLine($"Новый баланс: {accountToUpdate.Amount}");

                    return Data;
                }
                else
                {
                    Console.WriteLine("Аккаунт не найден");
                    return Data;
                }
            }
            else
            {
                Console.WriteLine("Двнного клиента или счета нет");
                return Data;
            }
        }
        // Удалить аккаунт
        public Client DeleteAccount(Client client, Account account)
        {            
            if (Data.TryGetValue(client, out List<Account> accountList))
            {
                // удаляем аккаунт из списка
                accountList.Remove(account);

                // удаляем AccountId из списка клиента
                client.IdAccounts.Remove(account.AccountId);

                Console.WriteLine("\n");
                Console.WriteLine($"Аккаунт Id {account.AccountId} удален");
                Console.WriteLine($"Все аккаунты пользователя: {string.Join(", ", client.IdAccounts)}");
            }
            else
            {
                Console.WriteLine("Клиент не найден в словаре Data.");
            }
            return client;
        }

        // полечение данных по фильтру
        public IEnumerable<Client> GetClients(Func<Client, bool> filter)
        {
            return clients.Where(filter);
        }
    }    
}
