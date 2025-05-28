using LibrarieModele;
using NivelModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NivelStocareDate
{
    public class AdministrareClienti
    {
        private string numeFisier;

        public AdministrareClienti(string numeFisier)
        {
            this.numeFisier = numeFisier;
        }

        public void AdaugaClient(Client client)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(numeFisier, true))
                {
                    sw.WriteLine(client.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la adăugarea clientului în fișier", ex);
            }
        }

        public List<Client> AfisareClienti()
        {
            Console.WriteLine($"Încerc să citesc din fișierul: {numeFisier}");
            List<Client> clienti = new List<Client>();
            try
            {
                if (!File.Exists(numeFisier))
                {
                    Console.WriteLine("Fișierul nu există!");
                    return clienti;
                }

                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        Console.WriteLine($"Linie citită: {linie}");
                        Client client = Client.FromString(linie);
                        if (client != null)
                        {
                            clienti.Add(client);
                            Console.WriteLine($"Client adăugat: {client.Nume} {client.Prenume}");
                        }
                        else
                        {
                            Console.WriteLine("Linie invalidă ignorată.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citire: {ex.Message}");
            }
            return clienti;
        }

        public Client CautaClient(string nume, string prenume)
        {
            try
            {
                if (!File.Exists(numeFisier))
                    return null;

                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        Client client = Client.FromString(linie);
                        if (client.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) &&
                            client.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase))
                            return client;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la căutarea clientului în fișier", ex);
            }
            return null;
        }

        public List<Client> CautaClient(string textCautare)
        {
            List<Client> clientiGasiti = new List<Client>();
            try
            {
                if (!File.Exists(numeFisier))
                    return clientiGasiti;

                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        Client client = Client.FromString(linie);
                        if (client.Nume.ToLower().Contains(textCautare.ToLower()) ||
                            client.Prenume.ToLower().Contains(textCautare.ToLower()) ||
                            client.Telefon.Contains(textCautare) ||
                            client.Email.ToLower().Contains(textCautare.ToLower()))
                        {
                            clientiGasiti.Add(client);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la căutarea clienților în fișier", ex);
            }
            return clientiGasiti;
        }

        public Client CautaClientDupaTelefon(string telefon)
        {
            try
            {
                if (!File.Exists(numeFisier))
                    return null;

                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        Client client = Client.FromString(linie);
                        if (client.Telefon.Equals(telefon))
                            return client;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la căutarea clientului după telefon în fișier", ex);
            }
            return null;
        }

        public Client CautaClientDupaEmail(string email)
        {
            try
            {
                if (!File.Exists(numeFisier))
                    return null;

                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        Client client = Client.FromString(linie);
                        if (client.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                            return client;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la căutarea clientului după email în fișier", ex);
            }
            return null;
        }

        public void ActualizeazaClient(Client clientNou, string numeVechi, string prenumeVechi)
        {
            try
            {
                if (!File.Exists(numeFisier))
                    throw new FileNotFoundException("Fișierul clienților nu există");

                List<string> linii = File.ReadAllLines(numeFisier).ToList();
                bool clientGasit = false;

                for (int i = 0; i < linii.Count; i++)
                {
                    Client client = Client.FromString(linii[i]);
                    if (client.Nume.Equals(numeVechi, StringComparison.OrdinalIgnoreCase) &&
                        client.Prenume.Equals(prenumeVechi, StringComparison.OrdinalIgnoreCase))
                    {
                        linii[i] = clientNou.ToString();
                        clientGasit = true;
                        break;
                    }
                }

                if (!clientGasit)
                    throw new Exception("Clientul nu a fost găsit pentru actualizare");

                File.WriteAllLines(numeFisier, linii);
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la actualizarea clientului în fișier", ex);
            }
        }

        public void StergeClient(string nume, string prenume)
        {
            try
            {
                if (!File.Exists(numeFisier))
                    throw new FileNotFoundException("Fișierul clienților nu există");

                List<string> linii = File.ReadAllLines(numeFisier).ToList();
                bool clientGasit = false;

                for (int i = linii.Count - 1; i >= 0; i--)
                {
                    Client client = Client.FromString(linii[i]);
                    if (client.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) &&
                        client.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase))
                    {
                        linii.RemoveAt(i);
                        clientGasit = true;
                    }
                }

                if (!clientGasit)
                    throw new Exception("Clientul nu a fost găsit pentru ștergere");

                File.WriteAllLines(numeFisier, linii);
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la ștergerea clientului din fișier", ex);
            }
        }

        public List<Client> CautaClienti(string valoare)
        {
            var clientiGasiti = new List<Client>();
            var toateClientii = AfisareClienti();

            if (valoare.Contains(' '))
            {
                var split = valoare.Split(new[] { ' ' }, 2);
                var client = CautaClient(split[0], split[1]);
                if (client != null)
                    clientiGasiti.Add(client);
            }

            var clientiNumePrenume = toateClientii.Where(c =>
                c.Nume.ToLower().Contains(valoare.ToLower()) ||
                c.Prenume.ToLower().Contains(valoare.ToLower())
            ).ToList();

            foreach (var client in clientiNumePrenume)
            {
                if (!clientiGasiti.Contains(client))
                    clientiGasiti.Add(client);
            }

            var clientTelefon = CautaClientDupaTelefon(valoare);
            if (clientTelefon != null && !clientiGasiti.Contains(clientTelefon))
                clientiGasiti.Add(clientTelefon);

            var clientEmail = CautaClientDupaEmail(valoare);
            if (clientEmail != null && !clientiGasiti.Contains(clientEmail))
                clientiGasiti.Add(clientEmail);

            return clientiGasiti;
        }
    }
}