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
    public class ClientStorage : IStorage // работа с клиентами
    {
        //в хранилище добавить readonly список для хранение клиентов банка, 
        public readonly List<Client> clients = new List<Client>();

        public Dictionary<Client, List<Account>> Data { set; get; } = new Dictionary<Client, List<Account>>();   // ????


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

                        clients.Add(newClient);

                        
                        Console.WriteLine("\n");
                        Console.WriteLine("Новое добавление");
                        Console.WriteLine($"Имя- {newClient.FirstName} Город- {newClient.Address} Возраст- {newClient.DateOfBirth.ToString("dd/MM/yyyy")} Телефон- {newClient.PhoneNumber} ");
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
        public void Update(Client client, string FirstName, string LastName, DateTime DateOfBirth, string Address, string Email, string PhoneNumber, string passportData = "")
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
                            Client updatedClient = new Client();
                            updatedClient.FirstName = FirstName;
                            updatedClient.LastName = LastName;
                            updatedClient.DateOfBirth = DateOfBirth;
                            updatedClient.Address = Address;
                            updatedClient.Email = Email;
                            updatedClient.PhoneNumber = PhoneNumber;
                            updatedClient.SetPasportData(passportData);

                            clients[index] = updatedClient;

                            Console.WriteLine("\n");
                            Console.WriteLine("Обновление");
                            Console.WriteLine($"Имя- {updatedClient.FirstName} Город- {updatedClient.Address} Возраст- {updatedClient.DateOfBirth.ToString("dd/MM/yyyy")} Телефон- {updatedClient.PhoneNumber} ");
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
            }

        }


        // удалить клиента
        public void Delete(Client client)
        {
            clients.Remove(client);
        }




        // полечение данных по фильтру
        public IEnumerable<Client> GetClients(Func<Client, bool> filter)
        {
            return clients.Where(filter);
        }
    }    
}
