using System;

namespace VoziloTest
{
    [TestClass]
    public class VoziloTest
    {
        [TestMethod]
        public void TestCrpalka()
        {
            Razred.Razred.Vozilo volvo = new Razred.Razred.Vozilo(60, 8, 40);
            volvo.Crpalka();
            Assert.AreEqual(60, volvo.Gorivo);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Kapaciteta tanka ne more biti negativna ali enaka 0!")]
        public void TestVoziloKapaciteta()
        {
            Razred.Razred.Vozilo bmw = new Razred.Razred.Vozilo(-70, 9, 25);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Poraba ne more biti negativna!")]
        public void TestVoziloPoraba()
        {
            Razred.Razred.Vozilo bmw2 = new Razred.Razred.Vozilo(70, -9, 25);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Goriva ne more biti v negativnih količinah!")]
        public void TestVoziloGorivo()
        {
            Razred.Razred.Vozilo bmw3 = new Razred.Razred.Vozilo(70, 9, -25);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Goriva ne more biti v negativnih količinah!")]
        public void TestVoziloGorivoSet()
        {
            Razred.Razred.Vozilo bmw4 = new Razred.Razred.Vozilo(70, 9, 25);
            bmw4.Gorivo = -2;
        }

        [TestMethod]
        public void TestPreostaliKilometri()
        {
            Razred.Razred.Vozilo mb = new Razred.Razred.Vozilo(50, 5, 45);
            Assert.AreEqual(900, mb.PreostaliKilometri);
        }

        [TestMethod]

        public void TestLahkoPrevozi()
        {
            Razred.Razred.Vozilo audi = new Razred.Razred.Vozilo(67, 10, 32);

            double[] razdalje = { 200, 0, 100, 60, 0, 100 };
            bool lahkoPrevozi = audi.LahkoPrevozi(razdalje);
            Assert.AreEqual(true, lahkoPrevozi);

            double[] razdalje2 = { 200, 0, 10000, 60, 0, 100 };
            bool lahkoPrevozi2 = audi.LahkoPrevozi(razdalje2);
            Assert.AreEqual(false, lahkoPrevozi2);

            double[] razdalje3 = { 200, 0, 0, 60, 0, 100 };
            Assert.ThrowsException<Exception>(() =>
            {
                audi.LahkoPrevozi(razdalje3);
            });

            double[] razdalje4 = { 200, 0, -100, 60, 0, 100 };
            Assert.ThrowsException<Exception>(() =>
            {
                audi.LahkoPrevozi(razdalje4);
            });
        }

    }
}


