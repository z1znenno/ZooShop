using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 3 — Karu (Ursus arctos).
    /// Lisab: kaal, talveuni.
    /// </summary>
    public class Karu : Imetaja
    {
        public double Kaal         { get; set; }
        public bool   OnTalveunes  { get; set; } = false;

        public Karu(string nimi, DateTime sünniaeg, Sugu sugu,
                    int näljatase, string karvavärvus, double kaal)
        {
            Nimi          = nimi;
            Sünniaeg      = sünniaeg;
            Sugu          = sugu;
            Näljatase     = näljatase;
            Karvavärvus   = karvavärvus;
            Kaal          = kaal;
        }

        public override void Söö()
        {
            if (OnTalveunes)
            {
                Console.WriteLine($"  💤 {Nimi} magab talveund ja ei söö.");
                return;
            }
            Console.WriteLine($"  🍯 {Nimi} sööb mett ja kalu.");
            Näljatase += 20;
        }

        public void AlustaTalveund()
        {
            OnTalveunes = true;
            Console.WriteLine($"  💤 {Nimi} läks talveunne.");
        }

        public void ÄrataTalveunest()
        {
            OnTalveunes = false;
            Console.WriteLine($"  ☀️  {Nimi} ärkas talveunest.");
        }

        public override string LiigiInfo()
            => base.LiigiInfo() + "\n" +
               $"  Kaal        : {Kaal} kg\n" +
               $"  Talveunes   : {(OnTalveunes ? "Jah" : "Ei")}";
    }
}
