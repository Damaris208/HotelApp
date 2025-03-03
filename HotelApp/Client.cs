using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp
{
    public class Client
    {
        public string Nume;
        public string Prenume;
        public string Telefon;
        public string Email;

        public Client()
        {
            Nume = string.Empty;
            Prenume = string.Empty;
            Telefon = string.Empty;
            Email = string.Empty;
        }

        public Client(string nume, string prenume, string telefon, string email)
        {
            Nume = nume;
            Prenume = prenume;
            Telefon = telefon;
            Email = email;
        }
        public string Info()
        {
            if (Nume == string.Empty || Prenume == string.Empty)
                return "CLIENT NESETAT";
            else
                return $"Client: {Nume} {Prenume}, Telefon: {Telefon}, Email: {Email}";
        }
    }
}
