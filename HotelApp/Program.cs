using HotelApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp
{
    class Program
    {
        static void Main()
        {
            Client client1 = new Client("Popescu", "Ion", "0722123456", "ion.popescu@email.com");
            Camera camera1 = new Camera(101, "Dublă");

            Console.WriteLine(client1.Info());
            Console.WriteLine(camera1.Info());

            camera1.EsteOcupata = true;
            Console.WriteLine(camera1.Info());
        }
    }
}

