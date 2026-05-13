using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 2 — Abstraktne klass: imetajad.
    /// Lisab: karvavärvus, tiinus, järglaste arv.
    /// Paljunemine: emane kandab järglasi, isane saadetakse teise loomaaeda.
    /// </summary>
    public abstract class Imetaja : Loom
    {
        public string Karvavärvus   { get; set; } = "Määramata";
        public bool   OnTiine       { get; set; } = false;
        public int    JärglasedKokku { get; set; } = 0;

        /// <summary>
        /// Registreerib tiinuse alguse — kutsutakse pärast isase tagasitulekut.
        /// </summary>
        public void AlustaTiinus()
        {
            if (Sugu != ZooShop.Sugu.Emane)
            {
                Console.WriteLine($"  ⚠️  {Nimi} ei ole emane.");
                return;
            }
            OnTiine = true;
            Console.WriteLine($"  🤰 {Nimi} on nüüd tiine.");
        }

        /// <summary>
        /// Sünnitab pojad — tiinus lõpeb, järglaste arv kasvab.
        /// </summary>
        public void SünnitaPoegasid(int arv)
        {
            if (!OnTiine)
            {
                Console.WriteLine($"  ⚠️  {Nimi} ei ole tiine.");
                return;
            }
            OnTiine        = false;
            JärglasedKokku += arv;
            Console.WriteLine($"  🍼 {Nimi} sünnitas {arv} poega! Järglasi kokku: {JärglasedKokku}.");
        }

        public override string LiigiInfo()
            => $"  Karvavärvus     : {Karvavärvus}\n" +
               $"  Tiine           : {(OnTiine ? "Jah" : "Ei")}\n" +
               $"  Järglased kokku : {JärglasedKokku}";
    }
}
