using NivelModele;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

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
            SalveazaCameraInFisier(camera);
        }

        public Camera CautaCameraDupaNumar(int numar)
        {
            return camere.FirstOrDefault(c => c.Numar == numar);
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

        public void SalveazaCameraInFisier(Camera camera)
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

                    if (date.Length < 3)
                    {
                        Console.WriteLine($"Linie invalida: {linie}");
                        continue;
                    }

                    try
                    {
                        int numar = int.Parse(date[0]);
                        TipCamera tip = (TipCamera)Enum.Parse(typeof(TipCamera), date[1]);
                        bool esteOcupata = bool.Parse(date[2]);

                        OptiuniCamera optiuni = OptiuniCamera.Niciuna;
                        for (int i = 3; i < date.Length; i++)
                        {
                            if (Enum.TryParse(date[i], out OptiuniCamera optiune))
                            {
                                optiuni |= optiune;
                            }
                            else
                            {
                                Console.WriteLine($"Avertisment: Optiunea '{date[i]}' nu este valida.");
                            }
                        }

                        Camera camera = new Camera(numar, tip, optiuni) { EsteOcupata = esteOcupata };
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
