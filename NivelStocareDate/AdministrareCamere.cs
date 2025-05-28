using NivelModele;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace NivelStocareDate
{
    public class AdministrareCamere
    {
        private string caleFisier;
        private List<Camera> camere;

        public AdministrareCamere(string caleFisier = null)
        {
            if (string.IsNullOrEmpty(caleFisier))
            {
                string numeFisier = System.Configuration.ConfigurationManager.AppSettings["NumeFisierCamere"] ?? "camere.txt";
                this.caleFisier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, numeFisier);
            }
            else
            {
                this.caleFisier = caleFisier;
            }

            string directory = Path.GetDirectoryName(this.caleFisier);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

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
        public List<Camera> AfisareCamereDisponibile()
        {
            return camere.Where(c => !c.EsteOcupata).ToList();
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
            try
            {
                // Verifică dacă fișierul poate fi accesat
                if (File.Exists(caleFisier))
                {
                    using (FileStream fs = File.Open(caleFisier, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        // Fișierul este accesibil
                    }
                }

                using (StreamWriter writer = new StreamWriter(caleFisier, append: true))
                {
                    writer.WriteLine(ConvertesteCameraLaText(camera));
                }
            }
            catch (IOException ex)
            {
                throw new Exception($"Fișierul {caleFisier} este folosit de alt proces sau nu are permisiuni de acces: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la salvarea camerei în fișier: {ex.Message}", ex);
            }
        }

        private void RescrieFisierCamere()
        {
            try
            {
                // Verifică dacă fișierul poate fi accesat
                if (File.Exists(caleFisier))
                {
                    using (FileStream fs = File.Open(caleFisier, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        // Fișierul este accesibil
                    }
                }

                using (StreamWriter writer = new StreamWriter(caleFisier, append: false))
                {
                    foreach (Camera camera in camere)
                    {
                        writer.WriteLine(ConvertesteCameraLaText(camera));
                    }
                }
            }
            catch (IOException ex)
            {
                throw new Exception($"Fișierul {caleFisier} este folosit de alt proces sau nu are permisiuni de acces: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la rescrierea fișierului camerelor: {ex.Message}", ex);
            }
        }

        private string ConvertesteCameraLaText(Camera camera)
        {
            return $"{camera.Id},{camera.Numar},{camera.Tip},{camera.EsteOcupata},{(int)camera.Optiuni}";
        }

        private void IncarcaCamereDinFisier()
        {
            try
            {
                // Creează fișierul dacă nu există
                if (!File.Exists(caleFisier))
                {
                    File.Create(caleFisier).Close();
                    return;
                }

                // Verifică dacă fișierul poate fi accesat
                using (FileStream fs = File.Open(caleFisier, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    // Fișierul este accesibil pentru citire
                }

                camere.Clear();

                using (StreamReader reader = new StreamReader(caleFisier))
                {
                    string linie;
                    while ((linie = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linie)) continue;

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
            catch (IOException ex)
            {
                throw new Exception($"Fișierul {caleFisier} este folosit de alt proces sau nu are permisiuni de acces: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la încărcarea camerelor din fișier: {ex.Message}", ex);
            }
        }

        public void StergeCamera(int numarCamera)
        {
            Camera cameraDeSters = camere.Find(c => c.Numar == numarCamera);
            if (cameraDeSters != null)
            {
                camere.Remove(cameraDeSters);
                RescrieFisierCamere();
            }
        }

        public List<Camera> CautaCamere(string valoare)
        {
            var camereGasite = new List<Camera>();

            if (int.TryParse(valoare, out int numarCamera))
            {
                var camera = CautaCameraDupaNumar(numarCamera);
                if (camera != null)
                    camereGasite.Add(camera);
            }

            if (Enum.TryParse(valoare, true, out TipCamera tip))
            {
                var camere = AfisareCamere();
                var camereTip = camere.Where(c => c.Tip == tip).ToList();
                foreach (var camera in camereTip)
                {
                    if (!camereGasite.Contains(camera))
                        camereGasite.Add(camera);
                }
            }

            return camereGasite;
        }
    }
}