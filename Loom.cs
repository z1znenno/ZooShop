using System;

namespace ZooShop
{
    /// <summary>
    /// TASE 1 — Abstraktne baasklass.
    /// Üldised andmed: identiteet, vanus (sünnikuupäeva järgi), näljatase, sugu, staatus.
    /// </summary>
    public abstract class Loom
    {
        private int _näljatase;

        // ── Identiteet ─────────────────────────────────────────────────────────
        public string       Nimi        { get; set; } = "Tundmatu";
        public DateTime     Sünniaeg    { get; set; } = DateTime.Today;
        public Sugu         Sugu        { get; set; } = Sugu.Teadmata;
        public LoomaStaatus Staatus     { get; set; } = LoomaStaatus.Kohal;

        // ── Vanus arvutatakse sünnikuupäeva järgi ─────────────────────────────
        public int Vanus => (DateTime.Today - Sünniaeg).Days / 365;

        // ── Näljatase 0–100 ────────────────────────────────────────────────────
        public int Näljatase
        {
            get => _näljatase;
            set => _näljatase = Math.Clamp(value, 0, 100);
        }

        // ── Abstraktsed meetodid ───────────────────────────────────────────────
        public abstract void Söö();
        public abstract string LiigiInfo();   // liigispetsiifilised andmed

        // ── Ühised meetodid ────────────────────────────────────────────────────
        public TervislikuSeisund GetTervislikuSeisund() => Näljatase switch
        {
            < 30 => TervislikuSeisund.Kriitiline,
            < 60 => TervislikuSeisund.Rahuldav,
            _    => TervislikuSeisund.Hea
        };

        public string TervisInfo() => GetTervislikuSeisund() switch
        {
            TervislikuSeisund.Kriitiline => "⚠️  Kriitiline",
            TervislikuSeisund.Rahuldav   => "〰️  Rahuldav",
            _                            => "✅  Hea"
        };

        public override string ToString()
            => $"[{GetType().Name,-10}] {Nimi,-12} | {Vanus,2}a " +
               $"| Sugu: {Sugu,-8} | Näljatase: {Näljatase,3} " +
               $"| Tervis: {TervisInfo(),-16} | Staatus: {Staatus}";
    }
}
