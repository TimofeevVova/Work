using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services.Exceptions;

namespace Services.Storage
{
    public class ClientStorage 
    {
        //в хранилище добавить readonly список для хранение клиентов банка, 
        public readonly List<Client> clients = new List<Client>();
        //реализуем публичный метод добавления новых клиентов;
        public void AddNewClient(string FirstName, string LastName, DateTime DateOfBirth, string Addres, string Email, string PhoneNumber, string passportData = "")
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

                        clients.Add(newClient);


                        Console.WriteLine("\n");
                        Console.WriteLine("Новое добавление");
                        Console.WriteLine($"Имя- {newClient.FirstName} Город- {newClient.Address} Возраст- {newClient.DateOfBirth} Телефон- {newClient.PhoneNumber} ");
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
        }

        //экземпляр готового хранилища передаем через параметры конструктора в сервис работы с клиентами;     
        // в сервисе реализуем метод получения выборки из хранилища с применением фильтра: ФИО, номер телефона, номер паспорта, диапазон дат рождения.
        public IEnumerable<Client> GetClients(Func<Client, bool> filter)
        {
            return clients.Where(filter);
        }
        
        // в классе тестов проверяем работу системы, добавляем клиентов в хранилище и делаем выборки из нее;
    }    
}
