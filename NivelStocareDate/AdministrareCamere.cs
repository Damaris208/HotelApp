using System;
using System.Collections.Generic;
using System.IO;
using NivelModele;

namespace NivelStocareDate
{
    public class AdministrareCamere
    {
        private string caleFisier;
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

        public Camera CautaCameraDupaId(int id)
        {
            return camere.Find(c => c.Id == id);
        }

        public Camera CautaCameraDupaNumar(int numar)
        {
            return camere.Find(c => c.Numar == numar);
        }

        public List<Camera> AfisareCamere()
        {
            return camere;
        }

        public bool ActualizeazaCamera(Camera cameraActualizata)
        {
            // Căutare după ID (care nu se schimbă)
            int index = camere.FindIndex(c => c.Id == cameraActualizata.Id);

            if (index >= 0)
            {
                // Actualizează camera în listă
                camere[index] = cameraActualizata;

                // Rescrie întreg fișierul cu lista actualizată
                RescrieFisierCamere();

                return true;
            }

            return false;
        }

        private void SalveazaCameraInFisier(Camera camera)
        {
            using (StreamWriter writer = new StreamWriter(caleFisier, append: true))
            {
                writer.WriteLine(ConvertesteCameraLaText(camera));
            }
        }

        private void RescrieFisierCamere()
        {
            using (StreamWriter writer = new StreamWriter(caleFisier, append: false))
            {
                foreach (Camera camera in camere)
                {
                    writer.WriteLine(ConvertesteCameraLaText(camera));
                }
            }
        }

        private string ConvertesteCameraLaText(Camera camera)
        {
            return $"{camera.Id},{camera.Numar},{camera.Tip},{camera.EsteOcupata},{(int)camera.Optiuni}";
        }

        private void IncarcaCamereDinFisier()
        {
            camere.Clear(); // Șterge lista existentă înainte de încărcare

            if (!File.Exists(caleFisier)) return;

            using (StreamReader reader = new StreamReader(caleFisier))
            {
                string linie;
                while ((linie = reader.ReadLine()) != null)
                {
                    var date = linie.Split(',');

                    if (date.Length < 4) continue;

                    try
                    {
                        int id = int.Parse(date[0]);
                        int numar = int.Parse(date[1]);
                        TipCamera tip = (TipCamera)Enum.Parse(typeof(TipCamera), date[2]);
                        bool esteOcupata = bool.Parse(date[3]);

                        OptiuniCamera optiuni = OptiuniCamera.Niciuna;
                        if (date.Length > 4)
                        {
                            int optiuniInt = int.Parse(date[4]);
                            optiuni = (OptiuniCamera)optiuniInt;
                        }

                        Camera camera = new Camera(id, numar, tip, optiuni, esteOcupata);
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