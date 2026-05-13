using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 2 — Abstraktne klass: linnud.
    /// Lisab: tiivaulatus, munade arv.
    /// Paljunemine: mõlemad osapooled hauduvad — emane muneb pärast kohtumist.
    /// </summary>
    public abstract class Lind : Loom
    {
        public string Tiivaulatus    { get; set; } = "Määramata";
        public int    MunadKokku     { get; set; } = 0;
        public int    PojadKokku     { get; set; } = 0;

        /// <summary>
        /// Emane muneb pärast kohtumist isasega.
        /// </summary>
        public void Munes(int munedeArv)
        {
            if (Sugu != ZooShop.Sugu.Emane)
            {
                Console.WriteLine($"  ⚠️  {Nimi} ei ole emane — ei saa muneda.");
                return;
            }
            MunadKokku += munedeArv;
            Console.WriteLine($"  🥚 {Nimi} munis {munedeArv} muna! Mune kokku: {MunadKokku}.");
        }

        /// <summary>
        /// Registreerib koorunud pojad.
        /// </summary>
        public void KoorusidPojad(int arv)
        {
            PojadKokku += arv;
            Console.WriteLine($"  🐣 {Nimi}: koorusid {arv} poega! Poegi kokku: {PojadKokku}.");
        }

        public override string LiigiInfo()
            => $"  Tiivaulatus  : {Tiivaulatus}\n" +
               $"  Mune kokku   : {MunadKokku}\n" +
               $"  Pojad kokku  : {PojadKokku}";
    }
}
