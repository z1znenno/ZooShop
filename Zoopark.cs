using System;
using System.Collections.Generic;
using System.Linq;

namespace ZooShop
{
    public class Zoopark
    {
        public string     Nimi   { get; }
        public List<Loom> Loomad { get; } = new();

        private readonly List<(string LoomaNimi, string Sihtkoht, DateTime Kuupäev)> _transpordid = new();

        public Zoopark(string nimi) => Nimi = nimi;

        // ── Lisa ──────────────────────────────────────────────────────────────
        public void LisaLoom(Loom loom)
        {
            Loomad.Add(loom);
            Console.WriteLine($"  ✅ {loom.GetType().Name} \"{loom.Nimi}\" lisati loomaaeda.");
        }

        // ── Söötmine ──────────────────────────────────────────────────────────
        public void SöödaKõiki()
        {
            var aktiivsed = Loomad.Where(l => l.Staatus == LoomaStaatus.Kohal).ToList();
            if (!aktiivsed.Any()) { Console.WriteLine("  Pole aktiivseid loomi."); return; }
            foreach (var l in aktiivsed) l.Söö();
        }

        public void SöödaÜht(string nimi)
        {
            var l = Leia(nimi);
            if (l == null) return;
            if (l.Staatus != LoomaStaatus.Kohal)
            { Console.WriteLine($"  ⚠️  {nimi} ei ole praegu loomaaias (staatus: {l.Staatus})."); return; }
            l.Söö();
        }

        // ── Transport paljunemiseks ───────────────────────────────────────────
        public void SaadaTeiseLoomaaeda(string loomaNimi, string sihtkoht)
        {
            var l = Leia(loomaNimi);
            if (l == null) return;
            if (l.Staatus != LoomaStaatus.Kohal)
            { Console.WriteLine($"  ⚠️  {loomaNimi} ei saa saata — staatus: {l.Staatus}."); return; }

            l.Staatus = LoomaStaatus.Transpordil;
            _transpordid.Add((l.Nimi, sihtkoht, DateTime.Today));
            Console.WriteLine($"  🚛 {l.Nimi} saadetud loomaaeda \"{sihtkoht}\" paljunemiseks ({DateTime.Today:dd.MM.yyyy}).");
        }

        public void VõtaTagasi(string loomaNimi)
        {
            var l = Leia(loomaNimi);
            if (l == null) return;
            if (l.Staatus != LoomaStaatus.Transpordil)
            { Console.WriteLine($"  ⚠️  {loomaNimi} ei ole transpordil."); return; }

            l.Staatus = LoomaStaatus.Kohal;
            Console.WriteLine($"  🏠 {l.Nimi} naasis loomaaeda.");

            if (l is Imetaja im && im.Sugu == Sugu.Emane)
            {
                Console.Write($"  Kas {im.Nimi} jäi tiineks? (j/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "j")
                    im.AlustaTiinus();
            }

            if (l is Lind ln && ln.Sugu == Sugu.Emane)
            {
                Console.Write($"  Mitu muna {ln.Nimi} munis? (0 = ei muninud): ");
                if (int.TryParse(Console.ReadLine(), out int arv) && arv > 0)
                    ln.Munes(arv);
            }
        }

        // ── Järglased ─────────────────────────────────────────────────────────
        public void RegistreeriJärglased(string emaNimi)
        {
            var l = Leia(emaNimi);
            if (l == null) return;

            if (l is Imetaja im)
            {
                Console.Write($"  Mitu poega {im.Nimi} sünnitab? ");
                if (int.TryParse(Console.ReadLine(), out int arv))
                    im.SünnitaPoegasid(arv);
            }
            else if (l is Lind ln)
            {
                Console.Write($"  Mitu poega koorusid {ln.Nimi} munadest? ");
                if (int.TryParse(Console.ReadLine(), out int arv))
                    ln.KoorusidPojad(arv);
            }
            else
            {
                Console.WriteLine($"  ⚠️  {emaNimi} pole imetaja ega lind.");
            }
        }

        // ── Kuvamine ──────────────────────────────────────────────────────────
        public void NäitaKõiki()
        {
            if (!Loomad.Any()) { Console.WriteLine("  Nimekiri on tühi."); return; }
            Console.WriteLine($"  Kokku: {Loomad.Count} looma\n");
            Console.WriteLine(new string('─', 95));
            foreach (var l in Loomad)
                Console.WriteLine("  " + l);
            Console.WriteLine(new string('─', 95));
        }

        public void NäitaDetailid(string nimi)
        {
            var l = Leia(nimi);
            if (l == null) return;
            Console.WriteLine($"\n  ── {l.Nimi} ({l.GetType().Name}) ──────────────────────");
            Console.WriteLine("  " + l);
            Console.WriteLine(l.LiigiInfo());
        }

        public void NäitaStatistika()
        {
            Console.WriteLine($"\n  ── STATISTIKA: {Nimi} ──────────────────────────────");
            Console.WriteLine($"  Loomi kokku    : {Loomad.Count}");
            Console.WriteLine($"  Kohal          : {Loomad.Count(l => l.Staatus == LoomaStaatus.Kohal)}");
            Console.WriteLine($"  Transpordil    : {Loomad.Count(l => l.Staatus == LoomaStaatus.Transpordil)}");
            Console.WriteLine($"  Kriitiline     : {Loomad.Count(l => l.GetTervislikuSeisund() == TervislikuSeisund.Kriitiline)}");

            if (Loomad.Any())
            {
                var vanim   = Loomad.OrderByDescending(l => l.Vanus).First();
                var näljane = Loomad.OrderBy(l => l.Näljatase).First();
                Console.WriteLine($"  Vanim loom     : {vanim.Nimi} ({vanim.Vanus}a)");
                Console.WriteLine($"  Näljaseim loom : {näljane.Nimi} (näljatase {näljane.Näljatase})");
            }

            if (_transpordid.Any())
            {
                Console.WriteLine($"\n  ── Transpordi ajalugu ───────────────────────────");
                foreach (var (n, siht, kp) in _transpordid)
                    Console.WriteLine($"  {kp:dd.MM.yyyy}  {n,-14} → {siht}");
            }
        }

        // ── Abimeetod ─────────────────────────────────────────────────────────
        private Loom? Leia(string nimi)
        {
            var l = Loomad.FirstOrDefault(
                x => x.Nimi.Equals(nimi, StringComparison.OrdinalIgnoreCase));
            if (l == null) Console.WriteLine($"  ❌ Looma \"{nimi}\" ei leitud.");
            return l;
        }
    }
}