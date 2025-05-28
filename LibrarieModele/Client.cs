using System;

namespace NivelModele
{
    public class Client
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

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

        public static Client FromString(string v)
        {
            var parts = v.Split(',');
            if (parts.Length < 4)
                throw new ArgumentException("Linie invalidă pentru client: " + v);

            return new Client(parts[0], parts[1], parts[2], parts[3]);
        }

        public override string ToString()
        {
            return $"{Nume},{Prenume},{Telefon},{Email}";
        }
    }
}
