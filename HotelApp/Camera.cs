namespace HotelApp
{
    public class Camera
    {
        public int Numar { get; set; }
        public string Tip { get; set; }
        public bool EsteOcupata { get; set; }

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
