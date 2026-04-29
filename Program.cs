namespace ZooShop
{
    class Program
    {
        // ── Цвета темы ──────────────────────────────────────────────
        static readonly ConsoleColor CLR_ACCENT  = ConsoleColor.Cyan;
        static readonly ConsoleColor CLR_TITLE   = ConsoleColor.White;
        static readonly ConsoleColor CLR_SUCCESS = ConsoleColor.Green;
        static readonly ConsoleColor CLR_ERROR   = ConsoleColor.Red;
        static readonly ConsoleColor CLR_MUTED   = ConsoleColor.DarkGray;
        static readonly ConsoleColor CLR_INPUT   = ConsoleColor.Yellow;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "🐾 ZooShop";

            List<Loom> loomad = new List<Loom>();
            bool jookseb = true;

            while (jookseb)
            {
                DrawMainMenu(loomad.Count);
                string valik = ReadInput("");

                switch (valik)
                {
                    case "1":
                        AddImetaja(loomad);
                        break;

                    case "2":
                        AddLind(loomad);
                        break;

                    case "0":
                        jookseb = false;
                        break;

                    default:
                        ShowError("Vale valik! Sisesta 0, 1 või 2.");
                        Pause();
                        break;
                }
            }

            FeedAll(loomad);
        }

        // ── Главное меню ─────────────────────────────────────────────
        static void DrawMainMenu(int count)
        {
            Console.Clear();
            DrawBox(new[]
            {
                "  ██████  ██████   ██████  ",
                "     ██  ██    ██ ██    ██ ",
                "    ██   ██    ██ ██    ██ ",
                "   ██    ██    ██ ██    ██ ",
                "  ██████  ██████   ██████  ",
            }, CLR_ACCENT);

            Write("  ", CLR_MUTED);
            Write("ZooShop", CLR_TITLE);
            Write(" — loomade haldussüsteem\n", CLR_MUTED);
            DrawLine();

            WriteLn($"  Nimekirjas: ", CLR_MUTED);
            Write($"{count}", CLR_ACCENT);
            Write(" looma\n", CLR_MUTED);
            DrawLine();

            DrawMenuItem("1", "Imetaja", "Lisa uus imetaja");
            DrawMenuItem("2", "Lind",    "Lisa uus lind");
            DrawLine();
            DrawMenuItem("0", "Välju",   "Söödamine ja väljumine");
            DrawLine();

            Write("\n  Valik → ", CLR_INPUT);
        }

        static void DrawMenuItem(string key, string label, string desc)
        {
            Write("  [", CLR_MUTED);
            Write(key, CLR_ACCENT);
            Write("]  ", CLR_MUTED);
            Write($"{label,-10}", CLR_TITLE);
            Write($"  {desc}\n", CLR_MUTED);
        }

        // ── Добавление imetaja ───────────────────────────────────────
        static void AddImetaja(List<Loom> loomad)
        {
            Console.Clear();
            DrawHeader("UUS IMETAJA", ConsoleColor.Cyan);

            try
            {
                Imetaja imetaja = new Imetaja();

                imetaja.Nimi        = AskString("Nimi");
                imetaja.Vanus       = AskInt("Vanus (aastates)", 1, 100);
                imetaja.Näljatase   = AskInt("Näljatase", 0, 100);
                imetaja.Karvavärvus = AskString("Karvavärvus");

                loomad.Add(imetaja);
                ShowSuccess($"✓  Imetaja '{imetaja.Nimi}' edukalt lisatud!");
            }
            catch (FormatException)
            {
                ShowError("Viga: arv sisestati valesti!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ShowError($"Viga: {ex.ParamName}");
            }
            catch (ArgumentException ex)
            {
                ShowError($"Viga: {ex.Message}");
            }

            Pause();
        }

        // ── Добавление lind ──────────────────────────────────────────
        static void AddLind(List<Loom> loomad)
        {
            Console.Clear();
            DrawHeader("UUS LIND", ConsoleColor.Cyan);

            try
            {
                Lind lind = new Lind();

                lind.Nimi        = AskString("Nimi");
                lind.Vanus       = AskInt("Vanus (aastates)", 1, 100);
                lind.Tiivaulatus = AskString("Tiivaulatus");

                loomad.Add(lind);
                ShowSuccess($"✓  Lind '{lind.Nimi}' edukalt lisatud!");
            }
            catch (FormatException)
            {
                ShowError("Viga: arv sisestati valesti!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ShowError($"Viga: {ex.ParamName}");
            }
            catch (ArgumentException ex)
            {
                ShowError($"Viga: {ex.Message}");
            }

            Pause();
        }

        // ── Кормёжка всех ────────────────────────────────────────────
        static void FeedAll(List<Loom> loomad)
        {
            Console.Clear();
            DrawHeader("LOOMADE SÖÖDAMINE", ConsoleColor.Green);

            if (loomad.Count == 0)
            {
                Write("\n  ", CLR_MUTED);
                WriteLn("Nimekirjas pole ühtegi looma.", CLR_MUTED);
            }
            else
            {
                foreach (Loom loom in loomad)
                {
                    DrawLine();
                    Write("  ", default);
                    Write($"▶  {loom.Nimi}", CLR_TITLE);
                    Write($"  ({loom.GetType().Name})\n", CLR_MUTED);
                    Write("     Näljatase : ", CLR_MUTED);
                    WriteLn(loom.GetTervislikuSeisund(), CLR_ACCENT);
                    Write("     ", default);
                    loom.Söö();
                }
            }

            DrawLine();
            Write("\n  ", CLR_MUTED);
            WriteLn("Programm lõpetatud. Head aega! 🐾\n", CLR_MUTED);
        }

        // ── Валидация ввода ──────────────────────────────────────────
        static string AskString(string label)
        {
            Write($"\n  {label}: ", CLR_INPUT);
            string val = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(val))
                throw new ArgumentException($"{label} ei tohi olla tühi!");
            return val;
        }

        static int AskInt(string label, int min, int max)
        {
            Write($"\n  {label} ({min}–{max}): ", CLR_INPUT);
            string raw = Console.ReadLine()?.Trim() ?? "";
            if (!int.TryParse(raw, out int val))
                throw new FormatException();
            if (val < min || val > max)
                throw new ArgumentOutOfRangeException($"{label} peab olema {min}–{max}!");
            return val;
        }

        // ── Вспомогательный ввод строки (без проверки) ───────────────
        static string ReadInput(string prompt)
        {
            if (!string.IsNullOrEmpty(prompt))
                Write(prompt, CLR_INPUT);
            return Console.ReadLine()?.Trim() ?? "";
        }

        // ── Отрисовка UI-элементов ───────────────────────────────────
        static void DrawHeader(string title, ConsoleColor color)
        {
            DrawLine();
            Write($"  ◆ {title}\n", color);
            DrawLine();
        }

        static void DrawLine()
        {
            Write("  " + new string('─', 44) + "\n", CLR_MUTED);
        }

        static void DrawBox(string[] lines, ConsoleColor color)
        {
            Console.WriteLine();
            foreach (var line in lines)
            {
                Write("  " + line + "\n", color);
            }
            Console.WriteLine();
        }

        static void ShowSuccess(string msg)
        {
            Console.WriteLine();
            Write($"  {msg}\n", CLR_SUCCESS);
        }

        static void ShowError(string msg)
        {
            Console.WriteLine();
            Write($"  ✗  {msg}\n", CLR_ERROR);
        }

        static void Pause()
        {
            Write("\n  ", CLR_MUTED);
            Write("Vajuta Enter, et jätkata...", CLR_MUTED);
            Console.ReadLine();
        }

        static void Write(string text, ConsoleColor color)
        {
            if (color != default)
                Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        static void WriteLn(string text, ConsoleColor color)
        {
            Write(text + "\n", color);
        }
    }
}