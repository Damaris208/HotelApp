using System;
using NivelModele;
using NivelStocareDate;

namespace HotelApp
{
    class Program
    {
        static void Main()
        {
            AdministrareClienti adminClienti = new AdministrareClienti();
            AdministrareCamere adminCamere = new AdministrareCamere();
            Client clientNou = new Client();
            string optiune;

            Console.WriteLine("\nMeniu:");
            Console.WriteLine("0. Citire client de la tastatura");
            Console.WriteLine("1. Salvare client in vectorul de obiecte");
            Console.WriteLine("2. Afisare clienti");
            Console.WriteLine("3. Cautare client dupa nume si prenume");
            Console.WriteLine("4. Adaugare camera");
            Console.WriteLine("5. Afisare camere");
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
                        Camera camera = CitireCamera();
                        adminCamere.AdaugaCamera(camera);
                        break;

                    case "5":
                        adminCamere.AfisareCamere();
                        break;

                    case "6":
                        Console.Write("Introduceti numarul camerei: ");
                        int numar = int.Parse(Console.ReadLine());
                        Camera cameraGasita = adminCamere.CautaCamera(numar);
                        Console.WriteLine(cameraGasita != null ? cameraGasita.Info() : "Camera nu a fost gasita.");
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

        static Camera CitireCamera()
        {
            Console.Write("Numar camera: ");
            int numar = int.Parse(Console.ReadLine());
            Console.Write("Tip (Single/Dubla/Tripla/Quad): ");
            string tip = Console.ReadLine();

            return new Camera(numar, tip);
        }
    }
}
