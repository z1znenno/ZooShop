using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 3 — Kotkas (Aquila chrysaetos).
    /// Lisab: lennukiirus, pesa asukoht.
    /// </summary>
    public class Kotkas : Lind
    {
        public double Lennukiirus  { get; set; }
        public string PesaAsukoht { get; set; } = "Määramata";

        public Kotkas(string nimi, DateTime sünniaeg, Sugu sugu,
                      int näljatase, string tiivaulatus,
                      double lennukiirus, string pesaAsukoht)
        {
            Nimi         = nimi;
            Sünniaeg     = sünniaeg;
            Sugu         = sugu;
            Näljatase    = näljatase;
            Tiivaulatus  = tiivaulatus;
            Lennukiirus  = lennukiirus;
            PesaAsukoht  = pesaAsukoht;
        }

        public override void Söö()
        {
            Console.WriteLine($"  🐟 {Nimi} sukeldub ja püüab kala.");
            Näljatase += 20;
        }

        public override string LiigiInfo()
            => base.LiigiInfo() + "\n" +
               $"  Lennukiirus : {Lennukiirus} km/h\n" +
               $"  Pesa        : {PesaAsukoht}";
    }
}
