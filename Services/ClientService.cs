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

namespace Services
{
    public class ClientService
    {
        //временно в качестве хранилища используем приватный словарь типа Dictionary<Client>,<List<Account>>;
        private static Dictionary<Client, List<Account>> Data = new Dictionary<Client, List<Account>>();


        // метод добавления новых клиентов (в методе предусмотреть валидацию);
        public static Dictionary<Client, List<Account>> AddNewClient(string FirstName, string LastName, DateTime DateOfBirth, string Addres, string passportData = "", string Email = "", string PhoneNumber = "")
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime MinAge = today.AddYears(-18);
                Client newClient = new Client();

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
                        // при добавлении нового клиента создаем ему дефолтный лицевой счет;

                        Console.WriteLine("\n");

                        CreateNewAccountFromClient(newClient);

                        Console.WriteLine($"Имя- {newClient.FirstName} Город- {newClient.Address} Возраст- {newClient.DateOfBirth} MinAge-{MinAge} Телефон- {newClient.PhoneNumber} ");

                        return Data;
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
                return Data;
            }
            catch (ExceptionNoPassportData e)
            {
                Console.WriteLine(e.ToString());
                return Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Data;
            }
        }

        // при добавлении нового клиента создаем ему новый дефолтный лицевой счет;
        public static void CreateNewAccountFromClient(Client client)
        {            
            Random random = new Random();

            // создание нового счета
            bool isDefault = true;
            Account account = TestDataGenerator.GenerateNewAccount(isDefault);
            
            // присвоение счета к клиенту
            List<Account> accountList = new List<Account>() { account };
            if (Data.ContainsKey(account))
            {
                // если ключ уже есть, обновляем значение
                Data[client] = accountList;
            }
            else
            {
                // если ключа нет, добавляем новую пару ключ-значение
                Data.Add(client, accountList);
            }
            Console.WriteLine("Создан счет для клиента");
        }

        // метод добавления дополнительного лицевого счета ранее зарегистрированному клиенту (соответствующая валидация);
        public static Dictionary<Client, List<Account>> CreateAdditionalAccountFromClient(Client client)
        {
            if (Data.ContainsKey(client))
            {
                // создание нового счета
                bool isDefault = false;
                Account additionalAccount = TestDataGenerator.GenerateNewAccount(isDefault);

                // Получаем существующий список аккаунтов клиента
                List<Account> accountList = Data[client];

                // Добавляем новый аккаунт к списку
                accountList.Add(additionalAccount);

                // Обновляем значение в словаре
                Data[client] = accountList;

                return Data;
            }
            else
            {
                return new Dictionary<Client, List<Account>>();
            }
        }

        // метод редактирования ранее добавленного лицевого счета (соответствующая валидация);
        public static Dictionary<Client, List<Account>> EditAccount(Client client)
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
                        accountList[0].Amount = 7000;
                        Console.WriteLine("Изменено на:");
                        Console.WriteLine($"ID - {id}, Баланс -  {accountList[0].Amount}");

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
                    Console.WriteLine("Данного клиента нет");
                    return Data;
                }
            }
            else
            {
                return Data;
            }            
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







        //● реализуем тесты для проверки функционала сервиса “ClientService”;


        //● самостоятельно реализовать аналогичный подход для работы с сотрудниками банка.
    }



    




}
