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
                Console.WriteLine("Nu exista camere inregistrate.");
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
                    writer.WriteLine($"{camera.Numar},{camera.Tip},{camera.EsteOcupata},{camera.Optiuni}");
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

                    if (date.Length < 4)
                    {
                        Console.WriteLine($"Linie invalidă: {linie}");
                        continue;
                    }

                    try
                    {
                        Camera camera = new Camera(int.Parse(date[0]), (TipCamera)Enum.Parse(typeof(TipCamera), date[1]))
                        {
                            EsteOcupata = bool.Parse(date[2]),
                            Optiuni = (OptiuniCamera)Enum.Parse(typeof(OptiuniCamera), date[3])
                        };

                        camere.Add(camera);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Eroare la procesarea liniei: {linie}. Detalii: {ex.Message}");
                    }
                }
            }
        }

    }
}
