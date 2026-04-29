namespace ZooShop
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Loom> loomad = new List<Loom>();
            bool jookseb = true;

            while (jookseb)
            {
                Console.WriteLine("\nVali loom: 1 - Imetaja, 2 - Lind, 0 - Välju");
                string valik = Console.ReadLine()?.Trim() ?? "";

                switch (valik)
                {
                    case "1":
                        try
                        {
                            Imetaja imetaja = new Imetaja();

                            Console.WriteLine("Sisesta imetaja nimi:");
                            imetaja.Nimi = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(imetaja.Nimi))
                                throw new ArgumentException("Nimi ei tohi olla tühi!");

                            Console.WriteLine("Sisesta imetaja vanus:");
                            imetaja.Vanus = int.Parse(Console.ReadLine());
                            if (imetaja.Vanus <= 0)
                                throw new ArgumentOutOfRangeException("Vanus peab olema positiivne arv!");

                            Console.WriteLine("Sisesta imetaja näljatase (0-100):");
                            imetaja.Näljatase = int.Parse(Console.ReadLine());
                            if (imetaja.Näljatase < 0 || imetaja.Näljatase > 100)
                                throw new ArgumentOutOfRangeException("Näljatase peab olema vahemikus 0-100!");

                            Console.WriteLine("Sisesta imetaja karvavärvus:");
                            imetaja.Karvavärvus = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(imetaja.Karvavärvus))
                                throw new ArgumentException("Karvavärvus ei tohi olla tühi!");

                            loomad.Add(imetaja);
                            Console.WriteLine($"Imetaja '{imetaja.Nimi}' lisatud!");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Viga: vanus ja näljatase peavad olema täisarvud!");
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine($"Viga: {ex.ParamName}");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Viga: {ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            Lind lind = new Lind();

                            Console.WriteLine("Sisesta linnu nimi:");
                            lind.Nimi = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(lind.Nimi))
                                throw new ArgumentException("Nimi ei tohi olla tühi!");

                            Console.WriteLine("Sisesta linnu vanus:");
                            lind.Vanus = int.Parse(Console.ReadLine());
                            if (lind.Vanus <= 0)
                                throw new ArgumentOutOfRangeException("Vanus peab olema positiivne arv!");

                            Console.WriteLine("Sisesta linnu tiivaulatus:");
                            lind.Tiivaulatus = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(lind.Tiivaulatus))
                                throw new ArgumentException("Tiivaulatus ei tohi olla tühi!");

                            loomad.Add(lind);
                            Console.WriteLine($"Lind '{lind.Nimi}' lisatud!");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Viga: vanus peab olema täisarv!");
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine($"Viga: {ex.ParamName}");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Viga: {ex.Message}");
                        }
                        break;

                    case "0":
                        jookseb = false;
                        break;

                    default:
                        Console.WriteLine("Vale valik, proovi uuesti.");
                        break;
                }
            }

            if (loomad.Count == 0)
            {
                Console.WriteLine("\nNimekirjas pole ühtegi looma.");
                return;
            }

            Console.WriteLine("\nLoomade söödamine:");
            foreach (Loom loom in loomad)
            {
                Console.WriteLine($"\n{loom.Nimi} ({loom.GetType().Name}) sööb:");
                Console.WriteLine($"Näljatase: {loom.GetTervislikuSeisund()}");
                loom.Söö();
            }
        }
    }
}