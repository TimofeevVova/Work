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
    //В рамках сервиса “ClientService” реализовать методы:
    //временно в качестве хранилища используем приватный словарь типа Dictionary<Client>,<List<Account>>;
    public class ClientService
    {
        // создадим приватный словарь
        private static Dictionary<Client, List<Account>> Data = new Dictionary<Client, List<Account>>();
        // метод добавления новых клиентов (в методе предусмотреть валидацию);
        public static Dictionary<Client, List<Account>> AddNewClient(string FirstName, string LastName, DateTime DateOfBirth, string Addres, string passportData="", string Email = "", string PhoneNumber = "")
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
                    Data = CreateNewAccountFromClient(newClient);
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

            return Data;
        }

        // при добавлении нового клиента создаем ему дефолтный лицевой счет;
        public static Dictionary<Client, List<Account>> CreateNewAccountFromClient(Client client)
        {
            Dictionary<Client, List<Account>> newData = new Dictionary<Client, List<Account>>();
            Random random = new Random();

            // создание нового счета
            Account account = new Account();
            {
                account.AccountId = random.Next(100, 9999);
                account.Currency = TestDataGenerator.GenerateCurrency();
                account.Amount = 0;
            }

            // присвоение счета к клиенту
            List<Account> accountList = new List<Account>() { account };
            newData[account] = accountList;

            return newData;
        }













        //● метод добавления дополнительного лицевого счета ранее зарегистрированному клиенту (соответствующая валидация);


        //● метод редактирования ранее добавленного лицевого счета (соответствующая валидация);


        //● реализуем тесты для проверки функционала сервиса “ClientService”;


        //● самостоятельно реализовать аналогичный подход для работы с сотрудниками банка.
    }
}
