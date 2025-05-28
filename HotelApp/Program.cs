using NivelModele;
using NivelStocareDate;
using System;

namespace HotelApp
{
    class Program
    {
        static void Main()
        {
            string caleFisierCamere = "camere.txt";
            string caleFisierClienti = "clienti.txt";
            AdministrareClienti adminClienti = new AdministrareClienti(caleFisierClienti);
            AdministrareCamere adminCamere = new AdministrareCamere(caleFisierCamere);
            Client clientNou = new Client();
            string optiune;

            Console.WriteLine("\nMeniu:");
            Console.WriteLine("0. Citire client de la tastatura");
            Console.WriteLine("1. Salvare client in vectorul de obiecte");
            Console.WriteLine("2. Afisare clienti");
            Console.WriteLine("3. Cautare client dupa nume si prenume");
            Console.WriteLine("4. Afisare camere");
            Console.WriteLine("5. Adaugare camera");
            Console.WriteLine("6. Cautare camera dupa numar");
            Console.WriteLine("X. Iesire");

            do
            {
                Console.Write("\nAlegeti o optiune: ");
                optiune = Console.ReadLine().ToUpper();

                switch (optiune)
                {
                    case "0":
                        clientNou = CitireClientTastatura();
                        break;

                    case "1":
                        adminClienti.AdaugaClient(clientNou);
                        break;

                    case "2":
                        adminClienti.AfisareClienti();
                        break;

                    case "3":
                        Console.Write("Introduceti numele clientului: ");
                        string nume = Console.ReadLine();
                        Console.Write("Introduceti prenumele clientului: ");
                        string prenume = Console.ReadLine();
                        Client clientGasit = adminClienti.CautaClient(nume, prenume);
                        Console.WriteLine(clientGasit != null ? clientGasit.Info() : "Clientul nu a fost gasit.");
                        break;

                    case "4":
                        adminCamere.AfisareCamere();
                        break;

                    case "5":
                        AdaugaCamera(adminCamere);
                        break;

                    case "6":
                        CautaCamera(adminCamere);
                        break;

                    case "X":
                        Console.WriteLine("Program incheiat.");
                        break;

                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
            } while (optiune != "X");
        }

        static Client CitireClientTastatura()
        {
            Console.Write("Nume: ");
            string nume = Console.ReadLine();
            Console.Write("Prenume: ");
            string prenume = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            return new Client(nume, prenume, telefon, email);
        }

        static void AdaugaCamera(AdministrareCamere adminCamere)
        {
            Console.Write("Numar camera: ");
            int numar = int.Parse(Console.ReadLine());

            Console.Write("Tip camera (Single/Double/Quad/Suite/Deluxe): ");
            string tipCameraInput = Console.ReadLine();
            TipCamera tipCamera = (TipCamera)Enum.Parse(typeof(TipCamera), tipCameraInput, true);

            OptiuniCamera optiuniCamera = OptiuniCamera.Niciuna;
            Console.WriteLine("Alege optiuni pentru camera (foloseste cifrele pentru a adauga):");
            Console.WriteLine("1. Aer Conditionat");
            Console.WriteLine("2. WiFi");
            Console.WriteLine("3. Frigider");
            Console.WriteLine("4. TV");
            Console.WriteLine("5. Balcon");
            Console.WriteLine("0. Termină selectia");

            while (true)
            {
                Console.Write("Alege o optiune: ");
                string optiune = Console.ReadLine();

                if (optiune == "0") break;

                switch (optiune)
                {
                    case "1": optiuniCamera |= OptiuniCamera.AerConditionat; break;
                    case "2": optiuniCamera |= OptiuniCamera.WiFi; break;
                    case "3": optiuniCamera |= OptiuniCamera.Frigider; break;
                    case "4": optiuniCamera |= OptiuniCamera.TV; break;
                    case "5": optiuniCamera |= OptiuniCamera.Balcon; break;
                    default: Console.WriteLine("Optiune invalida!"); break;
                }
            }

            Camera cameraNoua = new Camera(numar, tipCamera, optiuniCamera) { EsteOcupata = false };
            adminCamere.AdaugaCamera(cameraNoua);
            Console.WriteLine("Camera a fost adaugata cu succes.");
        }

        static void CautaCamera(AdministrareCamere adminCamere)
        {
            Console.Write("Introduceti numarul camerei cautate: ");
            if (!int.TryParse(Console.ReadLine(), out int numar))
            {
                Console.WriteLine("Numar invalid!");
                return;
            }

            Camera cameraGasita = adminCamere.CautaCameraDupaNumar(numar);

            if (cameraGasita != null)
            {
                Console.WriteLine("Camera gasita:");
                Console.WriteLine(cameraGasita.Info());
            }
            else
            {
                Console.WriteLine("Camera nu a fost gasita.");
            }
        }
    }
}
