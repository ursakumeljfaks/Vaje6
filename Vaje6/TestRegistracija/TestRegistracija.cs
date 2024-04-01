namespace TestRegistracija;

[TestClass]
public class TestRegistracija
{
    [TestMethod]
    public void TestObmocje()
    {
        Assert.ThrowsException<Exception>(() =>
        {
            Razred2.Razred2.Registracija r1 = new Razred2.Razred2.Registracija("ALI", "67677");
        });
        Assert.ThrowsException<Exception>(() =>
        {
            Razred2.Razred2.Registracija r1 = new Razred2.Razred2.Registracija("JJ", "67677");
        });
    }

    [TestMethod]
    public void TestRegistrska()
    {
        Assert.ThrowsException<Exception>(() =>
        {
            Razred2.Razred2.Registracija r1 = new Razred2.Razred2.Registracija("LJ", "77");
        });
    }

    [TestMethod]
    public void TestIzObmocja()
    {
        Random random = new Random();
        string[] obmocja = { "LJ", "KR", "KK", "MB", "MS", "KP", "GO", "CE", "SG", "NM", "PO" };
        string[] tabela = new string[100];
        for (int i = 0; i < 100; i++)
        {
            string obmocje = obmocja[random.Next(obmocja.Length)];
            string registrska = zgenerirajRegistrske(random);
            tabela[i] = obmocje + registrska;
        }

        foreach (string ob in obmocja)
        {
            string[] nova = Razred2.Razred2.Registracija.IzObmocja(tabela, ob);
            foreach (string pravaRegistrska in nova)
            {
                if (pravaRegistrska != "0")
                {
                    Assert.IsTrue(pravaRegistrska.StartsWith(ob));
                }
            }
        }

    }

    static string zgenerirajRegistrske(Random random)
    {
        string crke = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] registrska = new char[5];
        for (int i = 0; i < 5; i++)
        {
            registrska[i] = crke[random.Next(crke.Length)];
        }
        return string.Join("", registrska);
    }


}
