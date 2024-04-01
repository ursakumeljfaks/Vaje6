namespace Razred;
public class Razred
{
    public class Vozilo
    {
        private double gorivo;
        private double kapaciteta;
        private double poraba;

        public Vozilo(double kapaciteta, double poraba, double gorivo)
        {
            if (kapaciteta <= 0) { throw new Exception("Kapaciteta tanka ne more biti negativna ali enaka 0!"); }
            this.kapaciteta = kapaciteta;
            if (poraba <= 0) throw new Exception("Poraba ne more biti negativna!");
            this.poraba = poraba;
            if (gorivo < 0) throw new Exception("Goriva ne more biti v negativnih količinah!");
            this.Gorivo = gorivo;
        }

        public double Kapaciteta
        {
            get { return this.kapaciteta; }
            set {; }
        }

        public double Poraba
        {
            get { return this.poraba; }
            set {; }
        }

        public double Gorivo
        {
            get { return this.gorivo; }
            set
            {
                if (value < 0) throw new Exception("Goriva ne more biti v negativnih količinah!");
                this.gorivo = value;
            }
        }

        public double PreostaliKilometri
        {
            get { return this.Gorivo / this.Poraba * 100; }
        }

        public override string ToString()
        {
            return $"Vozilo(kapaciteta = {this.Kapaciteta}, poraba = {this.Poraba}L/100km, gorivo = {this.Gorivo}L, km s tem tankom = {this.PreostaliKilometri}km)";
        }

        public void Crpalka()
        {
            this.Gorivo = this.Kapaciteta;
        }

        static bool preveriNule(double[] tabela)
        {
            bool preveri = false;
            for (int i = 0; i < tabela.Length; i++)
            {
                if (tabela[i] == 0 && tabela[i + 1] == 0)
                {
                    preveri = false;
                    break;
                }
                else
                {
                    preveri = true;
                }
            }
            return preveri;
        }

        public bool LahkoPrevozi(double[] tabela)
        {
            bool lahko = true;
            double preostaloGorivo = this.Gorivo;

            bool preveri = preveriNule(tabela);
            if (preveri is false)
            {
                throw new Exception("Ne moreš tankati, če je tank poln!");
            }

            for (int i = 0; i < tabela.Length; i++)
            {
                if (tabela[i] < 0)
                {
                    throw new Exception("V tabeli ne smejo biti negativne vrednosti!");
                }
                else if (tabela[i] == 0)
                {
                    this.Crpalka();
                    preostaloGorivo = this.Gorivo;
                }
                else
                {
                    double porabaPoti = tabela[i] / 100 * this.Poraba;
                    if (preostaloGorivo - porabaPoti < 0)
                    {
                        lahko = false;
                        break;
                    }
                    else
                    {
                        preostaloGorivo -= porabaPoti;
                    }
                }
            }
            if (lahko is true)
            {
                this.Gorivo = preostaloGorivo;
            }

            return lahko;
        }
    }



    static void Main(string[] args)
    {
        Vozilo Volvo = new Vozilo(60, 8, 40);
        Console.WriteLine(Volvo.ToString());
        Volvo.Crpalka();
        Console.WriteLine(Volvo.ToString());
        double[] razdalje = { 200, 0, 100, 60, 0, 100 };
        bool lahkoPrevozi = Volvo.LahkoPrevozi(razdalje);
        Console.WriteLine("Vozilo lahko prevozi: " + lahkoPrevozi);
        Console.WriteLine(Volvo.Kapaciteta);
        Console.ReadKey();
    }
}

