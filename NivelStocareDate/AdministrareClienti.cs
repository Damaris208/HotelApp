using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NivelModele;

namespace NivelStocareDate
{
    public class AdministrareClienti
    {
        private string caleFisierClienti = "clienti.txt";
        private List<Client> clienti;

        public AdministrareClienti()
        {
            clienti = new List<Client>();
            IncarcaClientiDinFisier();
        }

        public void AdaugaClient(Client client)
        {
            clienti.Add(client);
            SalveazaClientiInFisier();
        }

        public void AfisareClienti()
        {
            if (clienti.Count == 0)
            {
                Console.WriteLine("Nu exista clienti inregistrati.");
                return;
            }

            Console.WriteLine("\nLista clientilor:");
            foreach (var client in clienti)
            {
                Console.WriteLine(client.Info());
            }
        }

        public Client CautaClient(string nume, string prenume)
        {
            return clienti.Find(c => c.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) && c.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase));
        }
        public Client CautaClientDupaTelefon(string telefon)
        {
            return clienti.FirstOrDefault(c => c.Telefon == telefon);
        }
        public Client CautaClientDupaEmail(string email)
        {
            return clienti.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        private void SalveazaClientiInFisier()
        {
            using (StreamWriter writer = new StreamWriter(caleFisierClienti))
            {
                foreach (var client in clienti)
                {
                    writer.WriteLine($"{client.Nume},{client.Prenume},{client.Telefon},{client.Email}");
                }
            }
        }

        private void IncarcaClientiDinFisier()
        {
            if (!File.Exists(caleFisierClienti)) return;

            using (StreamReader reader = new StreamReader(caleFisierClienti))
            {
                string linie;
                while ((linie = reader.ReadLine()) != null)
                {
                    var date = linie.Split(',');
                    Client client = new Client(date[0], date[1], date[2], date[3]);
                    clienti.Add(client);
                }
            }
        }
    }
}
