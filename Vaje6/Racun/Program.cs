namespace Racun;
class Racun
{
    private string valuta;
    private double stanje;
    private double tecaj;

    public Racun(string valuta, double tecaj, double stanje = 0)
    {
        HashSet<string> valute = new HashSet<string> { "USD", "AUD", "CHF", "GBP", "CZK", "CYP", "NOK", "SEK", "DKK" };
        if (!valute.Contains(valuta) || valuta.Length != 3)
        {
            throw new Exception("Ni ustrezna valuta!");
        }
        this.Valuta = valuta;
        this.Stanje = stanje;
        this.Tecaj = tecaj;
    }

    public string Valuta
    {
        get { return this.valuta; }
        set {; }
    }

    public double Stanje
    {
        get { return this.stanje; }
        set
        {
            // stanje je lahko negativno, zato ni nobenega preverjanja
            this.stanje = value;
        }
    }

    public double Tecaj
    {
        get { return this.tecaj; }
        set
        {
            if (value < 0) { throw new Exception("Menjalni tecaj ne more biti negativen!"); }
            this.tecaj = value;
        }
    }

    public double StanjeEUR
    {
        get { return this.Stanje * this.Tecaj; }
    }

    public void Polog(double znesek)
    {
        double novoStanje = znesek / this.Tecaj;
        this.Stanje += novoStanje;
    }

    static void Main(string[] args)
    {
        Racun r1 = new Racun("NOK", 0.085, 40000);
        r1.Polog(300);
        Console.WriteLine("Stanje v NOK: " + r1.Stanje);
        Console.WriteLine("Stanje v EUR: " + r1.StanjeEUR);

        Racun r2 = new Racun("KKK", 0.4, 30);

        Console.ReadKey();
    }
}

