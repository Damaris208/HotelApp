namespace NivelModele
{
    public class Camera
    {
        private static int urmatorulId = 1;
        public int Id { get; private set; }
        public int Numar { get; set; }
        public TipCamera Tip { get; set; }
        public bool EsteOcupata { get; set; }
        public OptiuniCamera Optiuni { get; set; }

        public Camera()
        {
            Id = urmatorulId++;
            Numar = 0;
            Tip = TipCamera.Single;
            EsteOcupata = false;
            Optiuni = OptiuniCamera.Niciuna;
        }

        public Camera(int numar, TipCamera tip, OptiuniCamera optiuni)
        {
            Id = urmatorulId++;
            Numar = numar;
            Tip = tip;
            EsteOcupata = false;
            Optiuni = optiuni;
        }

        public Camera(int numar, TipCamera tip, OptiuniCamera optiuni, bool esteOcupata)
        {
            Id = urmatorulId++;
            Numar = numar;
            Tip = tip;
            Optiuni = optiuni;
            EsteOcupata = esteOcupata;
        }

        public Camera(int id, int numar, TipCamera tip, OptiuniCamera optiuni, bool esteOcupata)
        {
            Id = id;
            Numar = numar;
            Tip = tip;
            Optiuni = optiuni;
            EsteOcupata = esteOcupata;

            if (id >= urmatorulId)
            {
                urmatorulId = id + 1;
            }
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
            return $"ID: {Id}, Camera {Numar}, Tip: {Tip}, Opțiuni: {Optiuni}, Stare: {stare}";
        }
    }
}