using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NivelModele;

namespace NivelStocareDate
{
    public class AdministrareClienti
    {
        private string caleFisierClienti;
        private List<Client> clienti;

        public AdministrareClienti(string caleFisier = "clienti.txt")
        {
            caleFisierClienti = caleFisier;
            clienti = new List<Client>();
            IncarcaClientiDinFisier();
        }

        public void AdaugaClient(Client client)
        {
            // Check if client already exists
            if (clienti.Any(c => c.Nume.Equals(client.Nume, StringComparison.OrdinalIgnoreCase) &&
                               c.Prenume.Equals(client.Prenume, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Clientul există deja în sistem.");
            }

            clienti.Add(client);
            SalveazaClientiInFisier();
        }

        public void ActualizeazaClient(Client clientActualizat)
        {
            // Find the client to update
            var clientExistent = clienti.FirstOrDefault(c =>
                c.Nume.Equals(clientActualizat.Nume, StringComparison.OrdinalIgnoreCase) &&
                c.Prenume.Equals(clientActualizat.Prenume, StringComparison.OrdinalIgnoreCase));

            if (clientExistent == null)
            {
                throw new Exception("Clientul nu a fost găsit pentru actualizare.");
            }

            // Update the client's properties
            clientExistent.Telefon = clientActualizat.Telefon;
            clientExistent.Email = clientActualizat.Email;

            SalveazaClientiInFisier();
        }

        public List<Client> AfisareClienti()
        {
            return clienti.OrderBy(c => c.Nume).ThenBy(c => c.Prenume).ToList();
        }

        public Client CautaClient(string nume, string prenume)
        {
            return clienti.Find(c =>
                c.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) &&
                c.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase));
        }

        public Client CautaClientDupaTelefon(string telefon)
        {
            return clienti.FirstOrDefault(c => c.Telefon == telefon);
        }

        public Client CautaClientDupaEmail(string email)
        {
            return clienti.FirstOrDefault(c =>
                c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        private void SalveazaClientiInFisier()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(caleFisierClienti))
                {
                    foreach (var client in clienti)
                    {
                        writer.WriteLine($"{client.Nume},{client.Prenume},{client.Telefon},{client.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la salvarea clientilor în fișier: " + ex.Message);
            }
        }

        private void IncarcaClientiDinFisier()
        {
            if (!File.Exists(caleFisierClienti))
            {
                // Create the file if it doesn't exist
                File.Create(caleFisierClienti).Close();
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(caleFisierClienti))
                {
                    string linie;
                    while ((linie = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linie)) continue;

                        var date = linie.Split(',');
                        if (date.Length != 4) continue; // Skip invalid lines

                        Client client = new Client(
                            date[0].Trim(),
                            date[1].Trim(),
                            date[2].Trim(),
                            date[3].Trim()
                        );
                        clienti.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la încărcarea clientilor din fișier: " + ex.Message);
            }
        }

        public bool StergeClient(string nume, string prenume)
        {
            var client = CautaClient(nume, prenume);
            if (client != null)
            {
                clienti.Remove(client);
                SalveazaClientiInFisier();
                return true;
            }
            return false;
        }
    }
}