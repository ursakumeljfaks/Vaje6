
namespace Razred2;
public class Razred2
{
    public class Kosarica<T>
    {
        private T objekt;

        public Kosarica(T objekt)
        {
            this.Objekt = objekt;
        }


        public T Objekt
        {
            get { return this.objekt; }
            set
            {
                this.objekt = value;
            }
        }

        public override string ToString()
        {
            if (typeof(T) == typeof(int))
            {
                return "V naši košarici je: " + string.Join(", ", this.objekt) + ".";
            }
            else
            {
                return "V naši košarici je: " + this.objekt + ".";
            }

        }

    }

    public class Registracija
    {
        private string obmocje;
        private string registrska;

        public Registracija(string obmocje, string registrska)
        {
            this.Obmocje = obmocje;
            this.Registrska = registrska;
        }

        public string Obmocje
        {
            get { return this.obmocje; }
            set
            {
                HashSet<string> veljavni = new HashSet<string> { "LJ", "KR", "KK", "MB", "MS", "KP", "GO", "CE", "SG", "NM", "PO" };
                if (veljavni.Contains(value) && value.Length == 2)
                {
                    this.obmocje = value;
                }
                else
                {
                    throw new Exception("Ni veljavno območje!");
                }
            }
        }

        public string Registrska
        {
            get { return this.registrska; }
            set
            {
                if (value.Length == 5)
                {
                    this.registrska = value;
                }
                else
                {
                    throw new Exception("Ni ustrezna registerska!");
                }
            }
        }

        public override string ToString()
        {
            return $"Registracija: {this.Obmocje} {this.Registrska}";
        }

        /// <summary>
        /// vnre tiste registerske stevilke, ki so iz danega obmocja
        /// </summary>
        /// <param name="tabela">tabela registerskih tablic</param>
        /// <param name="obmocje">dano obmocje</param>
        /// <returns>vrne tabelo nizov teh registerskih tablic, ki so iz tega obmocja</returns>
        public static string[] IzObmocja(string[] tabela, string obmocje)
        {
            string[] izObmocja = new string[tabela.Length];
            for (int i = 0; i < tabela.Length; i++)
            {
                string ob = tabela[i].Substring(0, 2);
                string re = tabela[i].Substring(2, 5);
                Registracija tablca = new Registracija(ob, re);
                if (tablca.Obmocje == obmocje)
                {
                    izObmocja[i] = tabela[i];
                }
                else
                {
                    izObmocja[i] = "0";
                }
            }
            return izObmocja;
        }
    }

    static void Main(string[] args)
    {
        // testi za kosarico
        Kosarica<string> niz = new Kosarica<string>("Ursa");
        Console.WriteLine(niz.ToString());
        Kosarica<int> stevilo = new Kosarica<int>(12);
        Console.WriteLine(stevilo.ToString());
        int[] tabela = { 1, 14, 55, 13 };
        Kosarica<int[]> tabelaInt = new Kosarica<int[]>(tabela);
        Console.WriteLine(tabelaInt.ToString());
        Kosarica<double> realno = new Kosarica<double>(12.786);
        Console.WriteLine(realno.ToString());
        Kosarica<Razred.Razred.Vozilo> vozilo = new Kosarica<Razred.Razred.Vozilo>(new Razred.Razred.Vozilo(60, 8, 40));
        Console.WriteLine(vozilo.ToString());

        Console.ReadKey();
    }
}


