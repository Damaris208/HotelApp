using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp
{
    public class Camera
    {
        public int Numar;
        public string Tip;
        public bool EsteOcupata;

        public Camera()
        {
            Numar = 0;
            Tip = string.Empty;
            EsteOcupata = false;
        }

        public Camera(int numar, string tip)
        {
            Numar = numar;
            Tip = tip;
            EsteOcupata = false;
        }

        public void OcupaCamera()
        {
            EsteOcupata = true;
        }

        public string Info()
        {
            string stare = EsteOcupata ? "Ocupată" : "Liberă";
            if (Tip == string.Empty)
                return "CAMERA NESETATĂ";
            else
                return $"Camera {Numar}, Tip: {Tip}, Stare: {stare}";
        }
    }
}
