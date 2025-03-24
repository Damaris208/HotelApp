namespace NivelModele
{
    public class Camera
    {
        public int Numar { get; set; }
        public TipCamera Tip { get; set; }
        public bool EsteOcupata { get; set; }
        public OptiuniCamera Optiuni { get; set; }
        public int V { get; }
        public TipCamera TipCamera { get; }

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

        public Camera(int v, TipCamera tipCamera)
        {
            V = v;
            TipCamera = tipCamera;
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
            string stare = EsteOcupata ? "Ocupată" : "Liberă";
            return $"Camera {Numar}, Tip: {Tip}, Opțiuni: {Optiuni}, Stare: {stare}";
        }
    }
}
