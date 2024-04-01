namespace Kolo;
class Kolo
{
    private int stPrestav;
    private string barva;
    private string tip;
    private int leto;
    private int stLjudi;

    public Kolo(int stPrestav, string barva, string tip, int leto, int stLjudi)
    {
        this.StPrestav = stPrestav;
        this.Barva = barva;
        this.Tip = tip;
        this.Leto = leto;
        this.StLjudi = stLjudi;
    }

    public int StPrestav
    {
        get { return this.stPrestav; }
        set
        {
            if (value <= 0) { throw new Exception("Kolo nima 0 ali manj prestav!"); }
            this.stPrestav = value;
        }
    }

    public string Barva
    {
        get { return this.barva; }
        set
        {
            this.barva = value;
        }
    }

    public string Tip
    {
        get { return this.tip; }
        set
        {
            HashSet<string> mozniTipi = new HashSet<string> { "gorsko", "cestno", "treking" };
            if (!mozniTipi.Contains(value)) { throw new Exception("To ni veljaven tip kolesa!"); }
            this.tip = value;
        }
    }

    public int Leto
    {
        get { return this.leto; }
        set
        {
            if (value <= 0) { throw new Exception("Leto pa ja ni negativno ali 0!"); }
            this.leto = value;
        }
    }

    public int StLjudi
    {
        get { return this.stLjudi; }
        set
        {
            if (value <= 0) { throw new Exception("Na kolesu se lahko pelje vsaj en!"); }
            this.stLjudi = value;
        }
    }

    public override string ToString()
    {
        return $"{this.Leto},{this.Barva},{this.Tip},{this.StPrestav},{this.stLjudi}";
    }

    public static void NaDatoteko(string izhodna, Kolo[] kolesa)
    {
        using (StreamWriter sw = File.CreateText(izhodna))
        {
            for (int i = 0; i < kolesa.Length; i++)
            {
                sw.WriteLine($"{kolesa[i].Leto},{kolesa[i].Barva},{kolesa[i].Tip},{kolesa[i].StPrestav},{kolesa[i].stLjudi}");
            }

        }
    }

    public static Kolo[] NarediKolesa(string vhodna)
    {
        List<Kolo> kolesaList = new List<Kolo>();

        using (StreamReader sr = File.OpenText(vhodna))
        {
            string vrstica;
            while ((vrstica = sr.ReadLine()) != null)
            {
                string[] posebi = vrstica.Split(",");
                Kolo kolo = new Kolo(int.Parse(posebi[3]), posebi[1], posebi[2], int.Parse(posebi[0]), int.Parse(posebi[4]));
                kolesaList.Add(kolo);
            }
        }

        return kolesaList.ToArray();
    }

    static void Main(string[] args)
    {
        /// tabela n-tih random koles
        static Kolo[] tabelaKoles(int n)
        {
            Random random = new Random();
            string[] tipi = { "gorsko", "cestno", "treking" };
            string[] barve = { "zelena", "bela", "roza", "modra", "oranzna", "crna", "rumena", "vijolicna", "rdeca", "siva" };
            Kolo[] tabelaKoles = new Kolo[n];

            for (int i = 0; i < n; i++)
            {
                string tip = tipi[random.Next(tipi.Length)];
                string barva = barve[random.Next(barve.Length)];
                int prestava = random.Next(1, 31);
                int ljudi = random.Next(1, 3);
                int leto = random.Next(1900, 2025);
                tabelaKoles[i] = new Kolo(prestava, barva, tip, leto, ljudi);
            }

            return tabelaKoles;
        }

        // zapis vseh koles na datoteko
        NaDatoteko("kolesa.txt", tabelaKoles(100));
        // izpis na zaslon vseh teh koles
        Kolo[] tabKoles = NarediKolesa("kolesa.txt");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(tabKoles[i].ToString());
        }
        Console.WriteLine();

        /// za tabelo koles in specificno barvo vrne stevilo koles te barve
        static int posamezneBarve(Kolo[] tabelaKoles, string barva)
        {
            int koliko = 0;
            for (int i = 0; i < tabelaKoles.Length; i++)
            {
                if (tabelaKoles[i].Barva == barva)
                {
                    koliko++;
                }
            }
            return koliko;
        }

        /// za tabelo koles izpise stevilo posamezne barve za vsako barvo
        static void izpisStevilBarv(Kolo[] tabelaKoles)
        {
            string[] barve = { "zelena", "bela", "roza", "modra", "oranzna", "crna", "rumena", "vijolicna", "rdeca", "siva" };
            for (int i = 0; i < barve.Length; i++)
            {
                Console.WriteLine($"{barve[i]}: {posamezneBarve(tabelaKoles, barve[i])}");
            }
        }

        izpisStevilBarv(tabKoles);
        Console.WriteLine();

        /// iz vhodne datoteke prebere kolesa in staro barvo zamenja z novo in prepise na novo datoteko izhodna
        static void prepisBarve(string vhodna, string izhodna, string staraBarva, string novaBarva)
        {
            using (StreamReader sr = File.OpenText(vhodna))
            {
                using (StreamWriter sw = File.CreateText(izhodna))
                {
                    string vrstica = "";
                    while ((vrstica = sr.ReadLine()) != null)
                    {
                        string[] posebi = vrstica.Split(",");
                        if (posebi[1] == staraBarva)
                        {
                            sw.WriteLine($"{posebi[0]},{novaBarva},{posebi[2]},{posebi[3]},{posebi[4]}");
                        }
                        else
                        {
                            sw.WriteLine($"{vrstica}");
                        }
                    }
                }
            }
        }

        // v novo datoteko prepisemo vsa kolesa brez rumene
        prepisBarve("kolesa.txt", "kolesaBrezRumene.txt", "rumena", "rdeca");
        // naredimo tabelo teh koles 
        Kolo[] tabBrezRumene = NarediKolesa("kolesaBrezRumene.txt");
        Console.WriteLine();
        // izpisemo barva: stevilo koles te barve
        izpisStevilBarv(tabBrezRumene);

        Console.WriteLine();

        // koliko ljudi se lahko hkrati pelje s cestnim kolesom => stLjudi > 1
        int sCestnim = 0;
        for (int i = 0; i < tabBrezRumene.Length; i++)
        {
            if (tabBrezRumene[i].StLjudi > 1)
            {
                sCestnim++;
            }
        }
        Console.WriteLine($"S cestnim kolesom se lahko hkrati pelje {sCestnim} ljudi na enkrat.");
        Console.WriteLine();

        static void odstraniStarejsaOd12(string vhodna, string izhodna)
        {
            using (StreamReader sr = File.OpenText(vhodna))
            {
                using (StreamWriter sw = File.CreateText(izhodna))
                {
                    string vrstica = "";
                    while ((vrstica = sr.ReadLine()) != null)
                    {
                        string[] posebi = vrstica.Split(",");
                        if (2012 <= int.Parse(posebi[0]) && int.Parse(posebi[0]) <= 2024)
                        {
                            sw.WriteLine($"{posebi[0]},{posebi[1]},{posebi[2]},{posebi[3]},{posebi[4]}");
                        }
                    }
                }
            }
        }

        // prepisemo na novo datoteko vsa kolesa mlajsa od 12 let
        odstraniStarejsaOd12("kolesaBrezRumene.txt", "mlada12.txt");

        Console.ReadKey();
    }
}


