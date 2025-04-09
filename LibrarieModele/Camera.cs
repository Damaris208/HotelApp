namespace NivelModele
{
    public class Camera
    {
        public int Numar { get; set; }
        public TipCamera Tip { get; set; }
        public bool EsteOcupata { get; set; }
        public OptiuniCamera Optiuni { get; set; }

        public Camera()
        {
            Numar = 0;
            Tip = TipCamera.Single;
            EsteOcupata = false;
            Optiuni = OptiuniCamera.Niciuna;
        }

        public Camera(int numar, TipCamera tip, OptiuniCamera optiuni)
        {
            Numar = numar;
            Tip = tip;
            EsteOcupata = false;
            Optiuni = optiuni;
        }

        public Camera(int numar, TipCamera tip, OptiuniCamera optiuni, bool esteOcupata)
        {
            Numar = numar;
            Tip = tip;
            Optiuni = optiuni;
            EsteOcupata = esteOcupata;
        }

        public void OcupaCamera()
        {
            EsteOcupata = true;
        }

        public bool AreOptiune(OptiuniCamera optiune)
        {
            return (Optiuni & optiune) == optiune;
        }

        public string Info()
        {
            string stare = EsteOcupata ? "Ocupata" : "Libera";
            return $"Camera {Numar}, Tip: {Tip}, Optiuni: {Optiuni}, Stare: {stare}";
        }
    }
}
