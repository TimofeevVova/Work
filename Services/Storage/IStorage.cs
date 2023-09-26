using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace Services.Storage
{
    public interface IStorage
    {
        //Add(T item)
        Client Add(string FirstName, string LastName, DateTime DateOfBirth, string Address, string Email, string PhoneNumber, string passportData = "");

        //Update(Titem)
        void Update(Client client, string FirstName, string LastName, DateTime DateOfBirth, string Address, string Email, string PhoneNumber, string passportData = "");

        //Delete(T item)
        void Delete(Client client);
    }   
}
