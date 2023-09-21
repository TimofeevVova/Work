using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using Services.Exceptions;

namespace Services
{
    public class ClientService
    {
        //временно в качестве хранилища используем приватный словарь типа Dictionary<Client>,<List<Account>>;
        private static Dictionary<Client, List<Account>> Data = new Dictionary<Client, List<Account>>();


        // метод добавления новых клиентов (в методе предусмотреть валидацию);
        public static Client AddNewClient(string FirstName, string LastName, DateTime DateOfBirth, string Addres, string passportData="", string Email = "", string PhoneNumber = "")
        {
            DateTime today = DateTime.Today;
            DateTime MinAge = today.AddYears(-18);
            Client newClient = new Client();
            
            if (MinAge < DateOfBirth)
            {
                if(passportData != "")
                {
                    newClient.FirstName = FirstName;
                    newClient.LastName = LastName;
                    newClient.DateOfBirth = DateOfBirth;
                    newClient.Address = Addres;
                    newClient.SetPasportData(passportData);
                    newClient.Email = Email;
                    newClient.PhoneNumber = PhoneNumber;
                    // при добавлении нового клиента создаем ему дефолтный лицевой счет;
                    CreateNewAccountFromClient(newClient);

                    Console.WriteLine("\n");
                    Console.WriteLine($"Имя- {newClient.FirstName} Город- {newClient.Address} Телефон- {newClient.PhoneNumber} ");

                    return newClient;
                }                 
                else
                {
                    // выбрасываем исключение если у клиента нет паспортных данных;
                    throw new ExceptinoNoPassportData();
                }
            }
            else
            {
                // выбрасываем исключение если клиент моложе 18 лет
                throw new ExceptionAge();
            }            
        }

        // при добавлении нового клиента создаем ему дефолтный лицевой счет;
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
            return null;
        }


        // метод редактирования ранее добавленного лицевого счета (соответствующая валидация);











        //● реализуем тесты для проверки функционала сервиса “ClientService”;


        //● самостоятельно реализовать аналогичный подход для работы с сотрудниками банка.
    }
}
