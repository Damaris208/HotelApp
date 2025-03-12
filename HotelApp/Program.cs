using System;

namespace HotelApp
{
    class Program
    {
        static void Main()
        {
            AdministrareHotel adminHotel = new AdministrareHotel();

            string optiune;
            do
            {
                Console.WriteLine("\nMeniu:");
                Console.WriteLine("1. Adaugare client");
                Console.WriteLine("2. Afisare clienti");
                Console.WriteLine("3. Cautare client dupa nume");
                Console.WriteLine("4. Adaugare camera");
                Console.WriteLine("5. Afisare camere");
                Console.WriteLine("6. Cautare camera dupa numar");
                Console.WriteLine("X. Iesire");

                Console.Write("Alegeti o optiune: ");
                optiune = Console.ReadLine().ToUpper();

                switch (optiune)
                {
                    case "1":
                        Client client = CitireClient();
                        adminHotel.AdaugaClient(client);
                        break;

                    case "2":
                        adminHotel.AfisareClienti();
                        break;

                    case "3":
                        Console.Write("Introduceti numele clientului: ");
                        string nume = Console.ReadLine();
                        Console.Write("Introduceti prenumele clientului: ");
                        string prenume = Console.ReadLine();
                        Client clientGasit = adminHotel.CautaClient(nume, prenume);
                        Console.WriteLine(clientGasit != null ? clientGasit.Info() : "Clientul nu a fost gasit.");
                        break;

                    case "4":
                        Camera camera = CitireCamera();
                        adminHotel.AdaugaCamera(camera);
                        break;

                    case "5":
                        adminHotel.AfisareCamere();
                        break;

                    case "6":
                        Console.Write("Introduceti numarul camerei: ");
                        int numar = int.Parse(Console.ReadLine());
                        Camera cameraGasita = adminHotel.CautaCamera(numar);
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

        static Client CitireClient()
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
