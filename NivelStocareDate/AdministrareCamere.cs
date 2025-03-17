using System;
using System.Collections.Generic;
using System.IO;
using NivelModele;

namespace NivelStocareDate
{
    public class AdministrareCamere
    {
        private string caleFisier = "camere.txt";
        private List<Camera> camere;

        public AdministrareCamere()
        {
            camere = new List<Camera>();
            IncarcaCamereDinFisier();
        }

        public void AdaugaCamera(Camera camera)
        {
            camere.Add(camera);
            SalveazaCamereInFisier();
        }

        public void AfisareCamere()
        {
            if (camere.Count == 0)
            {
                Console.WriteLine("Nu există camere înregistrate.");
                return;
            }

            Console.WriteLine("\nLista camerelor:");
            foreach (var camera in camere)
            {
                Console.WriteLine(camera.Info());
            }
        }

        public Camera CautaCamera(int numar)
        {
            return camere.Find(c => c.Numar == numar);
        }

        private void SalveazaCamereInFisier()
        {
            using (StreamWriter writer = new StreamWriter(caleFisier))
            {
                foreach (var camera in camere)
                {
                    writer.WriteLine($"{camera.Numar},{camera.Tip},{camera.EsteOcupata}");
                }
            }
        }

        private void IncarcaCamereDinFisier()
        {
            if (!File.Exists(caleFisier)) return;

            using (StreamReader reader = new StreamReader(caleFisier))
            {
                string linie;
                while ((linie = reader.ReadLine()) != null)
                {
                    var date = linie.Split(',');
                    Camera camera = new Camera(int.Parse(date[0]), date[1])
                    {
                        EsteOcupata = bool.Parse(date[2])
                    };
                    camere.Add(camera);
                }
            }
        }
    }
}
