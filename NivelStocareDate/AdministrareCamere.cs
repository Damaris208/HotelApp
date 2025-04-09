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

        public AdministrareCamere(string caleFisier)
        {
            this.caleFisier = caleFisier;
            camere = new List<Camera>();
            IncarcaCamereDinFisier();
        }

        public void AdaugaCamera(Camera camera)
        {
            camere.Add(camera);
            SalveazaCameraInFisier(camera);
        }

        public Camera CautaCameraDupaNumar(int numar)
        {
            return camere.Find(c => c.Numar == numar);
        }

        public List<Camera> AfisareCamere()
        {
            if (camere.Count == 0)
            {
                Console.WriteLine("Nu exista camere inregistrate.");
                return new List<Camera>();  // Return an empty list if no rooms are available
            }

            return camere;  // Return the list of rooms
        }


        private void SalveazaCameraInFisier(Camera camera)
        {
            using (StreamWriter writer = new StreamWriter(caleFisier, append: true))
            {
                writer.WriteLine($"{camera.Numar},{camera.Tip},{camera.EsteOcupata},{(int)camera.Optiuni}");
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

                    if (date.Length < 3) continue;

                    try
                    {
                        int numar = int.Parse(date[0]);
                        TipCamera tip = (TipCamera)Enum.Parse(typeof(TipCamera), date[1]);
                        bool esteOcupata = bool.Parse(date[2]);

                        OptiuniCamera optiuni = OptiuniCamera.Niciuna;
                        if (date.Length > 3)
                        {
                            int optiuniInt = int.Parse(date[3]);
                            optiuni = (OptiuniCamera)optiuniInt;
                        }

                        Camera camera = new Camera(numar, tip, optiuni) { EsteOcupata = esteOcupata };
                        camere.Add(camera);  // Adăugăm camera în lista de camere
                        Console.WriteLine($"Camera adăugată: {camera.Info()}");  // Mesaj de debug
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
