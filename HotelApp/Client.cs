namespace HotelApp
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
    }
}
