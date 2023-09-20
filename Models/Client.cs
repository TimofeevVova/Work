using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client : Person //Class Клиент  (Айди Маил Телефон)
    {
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // переопределение
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Client other = (Client)obj;
            return ClientId == other.ClientId;
        }

        public override int GetHashCode()
        {
            return ClientId.GetHashCode();
        }
    }
}
