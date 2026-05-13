using System;
using System.Collections.Generic;
using System.Linq;

namespace ZooShop
{
    class Program
    {
        static readonly Zoopark zoo = new("Tallinna Loomaaed");

        static void Main()
        {
            LisaDemoLoomad();

            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║      🦁  LOOMAAED — HALDUSSÜSTEEM  🦅   ║");
            Console.WriteLine($"║      {zoo.Nimi,-36}║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            bool töötas = true;
            while (töötas)
                töötas = PeamenüüKäivita();
        }

        static bool PeamenüüKäivita()
        {
            Console.WriteLine();
            Console.WriteLine("┌────────────────────────────────────────────┐");
            Console.WriteLine("│                 PEAMENÜÜ                   │");
            Console.WriteLine("├────────────────────────────────────────────┤");
            Console.WriteLine("│  1 — Lisa loom                             │");
            Console.WriteLine("│  2 — Kuva kõik loomad                      │");
            Console.WriteLine("│  3 — Looma detailid                        │");
            Console.WriteLine("│  4 — Söötmine                              │");
            Console.WriteLine("│  5 — Transport (paljunemine)               │");
            Console.WriteLine("│  6 — Registreeri järglased                 │");
            Console.WriteLine("│  7 — Statistika                            │");
            Console.WriteLine("│  0 — Välju                                 │");
            Console.WriteLine("└────────────────────────────────────────────┘");
            Console.Write("  Vali: ");

            return Console.ReadLine()?.Trim() switch
            {
                "1" => LisaLoomMenuu(),
                "2" => KuvaKõik(),
                "3" => LoomaDetailid(),
                "4" => SöötmineMenuu(),
                "5" => TransportMenuu(),
                "6" => JärglasedMenuu(),
                "7" => Statistika(),
                "0" => Välju(),
                _   => ValeValik()
            };
        }

        // ── 1. Lisa loom ──────────────────────────────────────────────────────
        static bool LisaLoomMenuu()
        {
            Console.WriteLine("\n── LOOMA LISAMINE ────────────────────────────");
            Console.WriteLine("  1-Lõvi  2-Karu  3-Kotkas  4-Papagoi");
            Console.Write("  Vali liik: ");
            string liik = Console.ReadLine()?.Trim() ?? "";

            try
            {
                string   nimi      = LoeString("Nimi");
                DateTime sünniaeg  = LoeSünniaeg();
                Sugu     sugu      = LoeSugu();
                int      nälja     = LoeInt("Näljatase (0–100)");

                Loom uus = liik switch
                {
                    "1" => new Lovi(nimi, sünniaeg, sugu, nälja,
                               LoeString("Karvavärvus"),
                               LoeString("Territoorium (nt Savann A1)")),
                    "2" => new Karu(nimi, sünniaeg, sugu, nälja,
                               LoeString("Karvavärvus"),
                               LoeDouble("Kaal (kg)")),
                    "3" => new Kotkas(nimi, sünniaeg, sugu, nälja,
                               LoeString("Tiivaulatus (nt 2.5 m)"),
                               LoeDouble("Lennukiirus (km/h)"),
                               LoeString("Pesa asukoht")),
                    "4" => new Papagoi(nimi, sünniaeg, sugu, nälja,
                               LoeString("Tiivaulatus"),
                               LoeString("Sulestiku värv")),
                    _   => throw new Exception("Tundmatu liik.")
                };

                zoo.LisaLoom(uus);
            }
            catch (Exception e)
            {
                Console.WriteLine($"  ❌ Viga: {e.Message}");
            }
            return true;
        }

        // ── 2. Kuva kõik ──────────────────────────────────────────────────────
        static bool KuvaKõik()
        {
            Console.WriteLine("\n── KÕIK LOOMAD ───────────────────────────────");
            zoo.NäitaKõiki();
            return true;
        }

        // ── 3. Looma detailid ─────────────────────────────────────────────────
        static bool LoomaDetailid()
        {
            Console.WriteLine("\n── LOOMA DETAILID ────────────────────────────");
            zoo.NäitaDetailid(LoeString("Looma nimi"));
            return true;
        }

        // ── 4. Söötmine ───────────────────────────────────────────────────────
        static bool SöötmineMenuu()
        {
            Console.WriteLine("\n── SÖÖTMINE ──────────────────────────────────");
            Console.WriteLine("  1 — Sööda kõiki");
            Console.WriteLine("  2 — Sööda ühte looma");
            Console.Write("  Vali: ");
            switch (Console.ReadLine()?.Trim())
            {
                case "1": zoo.SöödaKõiki();                        break;
                case "2": zoo.SöödaÜht(LoeString("Looma nimi"));  break;
                default:  Console.WriteLine("  Vale valik.");       break;
            }
            return true;
        }

        // ── 5. Transport ──────────────────────────────────────────────────────
        static bool TransportMenuu()
        {
            Console.WriteLine("\n── TRANSPORT / PALJUNEMINE ───────────────────");
            Console.WriteLine("  1 — Saada loom teise loomaaeda");
            Console.WriteLine("  2 — Võta loom tagasi");
            Console.Write("  Vali: ");
            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    zoo.SaadaTeiseLoomaaeda(
                        LoeString("Looma nimi"),
                        LoeString("Sihtkoha loomaaed"));
                    break;
                case "2":
                    zoo.VõtaTagasi(LoeString("Looma nimi"));
                    break;
                default:
                    Console.WriteLine("  Vale valik.");
                    break;
            }
            return true;
        }

        // ── 6. Järglased ──────────────────────────────────────────────────────
        static bool JärglasedMenuu()
        {
            Console.WriteLine("\n── JÄRGLASED ─────────────────────────────────");
            zoo.RegistreeriJärglased(LoeString("Ema looma nimi"));
            return true;
        }

        // ── 7. Statistika ─────────────────────────────────────────────────────
        static bool Statistika()
        {
            zoo.NäitaStatistika();
            return true;
        }

        // ── 0. Välju ──────────────────────────────────────────────────────────
        static bool Välju()
        {
            Console.WriteLine("\n  Head aega! 🐾");
            return false;
        }

        static bool ValeValik()
        {
            Console.WriteLine("  ⚠️  Tundmatu valik, proovi uuesti.");
            return true;
        }

        // ── Sisendi abimeetodid ───────────────────────────────────────────────
        static string LoeString(string küsimus)
        {
            Console.Write($"  {küsimus}: ");
            return Console.ReadLine()?.Trim() is { Length: > 0 } s ? s : "Määramata";
        }

        static int LoeInt(string küsimus)
        {
            Console.Write($"  {küsimus}: ");
            return int.Parse(Console.ReadLine()?.Trim() ?? "0");
        }

        static double LoeDouble(string küsimus)
        {
            Console.Write($"  {küsimus}: ");
            return double.Parse(Console.ReadLine()?.Trim() ?? "0",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        static Sugu LoeSugu()
        {
            Console.Write("  Sugu (1-Isane / 2-Emane / 3-Teadmata): ");
            return Console.ReadLine()?.Trim() switch
            {
                "1" => Sugu.Isane,
                "2" => Sugu.Emane,
                _   => Sugu.Teadmata
            };
        }

        static DateTime LoeSünniaeg()
        {
            Console.Write("  Sünniaeg (pp.kk.aaaa): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dt)) return dt;
            Console.WriteLine("  (Vigane kuupäev, kasutan tänast.)");
            return DateTime.Today;
        }

        // ── Demo-andmed ───────────────────────────────────────────────────────
        static void LisaDemoLoomad()
        {
            zoo.LisaLoom(new Lovi("Simba",    new DateTime(2019, 3, 10), Sugu.Isane,  70, "Kuldne",    "Savann A1") { OnKarjajuht = true });
            zoo.LisaLoom(new Lovi("Nala",     new DateTime(2020, 6,  5), Sugu.Emane,  55, "Helepruun", "Savann A1"));
            zoo.LisaLoom(new Karu("Misha",    new DateTime(2016, 9, 20), Sugu.Isane,  40, "Pruun",     180.0));
            zoo.LisaLoom(new Karu("Lumivalk", new DateTime(2021, 1, 15), Sugu.Emane,  80, "Valge",     120.0));
            zoo.LisaLoom(new Kotkas("Taavi",  new DateTime(2018, 4,  2), Sugu.Isane,  60, "2.4 m",     150.0, "Torn B"));
            zoo.LisaLoom(new Papagoi("Lora",  new DateTime(2022, 7, 30), Sugu.Emane,  90, "0.4 m",    "Punane-roheline"));
        }
    }
}