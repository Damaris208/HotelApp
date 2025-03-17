using System;
using System.Collections.Generic;
using NivelModele;

namespace NivelStocareDate
{
    public class AdministrareClienti 
    {
        private List<Client> clienti = new List<Client>();

        public void AdaugaClient(Client client)
        {
            clienti.Add(client);
        }

        public void AfisareClienti()
        {
            foreach (var client in clienti)
            {
                Console.WriteLine(client.Info());
            }
        }

        public Client CautaClient(string nume, string prenume)
        {
            return clienti.Find(c => c.Nume == nume && c.Prenume == prenume);
        }
    }
}
