using System;
using System.Linq;

namespace HotelApp
{
    public class AdministrareHotel
    {
        private const int MaxNrClienti = 50;
        private const int MaxNrCamere = 50;

        private Client[] clienti;
        private Camera[] camere;

        private int nrClienti;
        private int nrCamere;

        public AdministrareHotel()
        {
            clienti = new Client[MaxNrClienti];
            camere = new Camera[MaxNrCamere];
            nrClienti = 0;
            nrCamere = 0;
        }

        public void AdaugaClient(Client client)
        {
            if (nrClienti < MaxNrClienti)
            {
                clienti[nrClienti] = client;
                nrClienti++;
            }
            else
            {
                Console.WriteLine("Nu se mai pot adauga clienti!");
            }
        }

        public void AdaugaCamera(Camera camera)
        {
            if (nrCamere < MaxNrCamere)
            {
                camere[nrCamere] = camera;
                nrCamere++;
            }
            else
            {
                Console.WriteLine("Nu se mai pot adauga camere!");
            }
        }

        public void AfisareClienti()
        {
            if (nrClienti == 0)
            {
                Console.WriteLine("Nu exista clienti inregistrati.");
                return;
            }

            Console.WriteLine("Lista clientilor:");
            foreach (var client in clienti.Take(nrClienti))
            {
                Console.WriteLine(client.Info());
            }
        }

        public void AfisareCamere()
        {
            if (nrCamere == 0)
            {
                Console.WriteLine("Nu există camere inregistrate.");
                return;
            }

            Console.WriteLine("Lista camerelor:");
            foreach (var camera in camere.Take(nrCamere))
            {
                Console.WriteLine(camera.Info());
            }
        }

        public Client CautaClient(string nume, string prenume)
        {
            return clienti.FirstOrDefault(c => c.Nume == nume && c.Prenume == prenume);
        }

        public Camera CautaCamera(int numar)
        {
            return camere.FirstOrDefault(c => c.Numar == numar);
        }
    }
}
