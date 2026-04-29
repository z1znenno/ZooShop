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
                Console.WriteLine("Vali loom: 1 - Imetaja, 2 - Lind, 0 - Välju");
                string valik = Console.ReadLine();

                switch (valik)
                {
                    case "1":
                        Imetaja imetaja = new Imetaja();
                        Console.WriteLine("Sisesta imetaja nimi:");
                        imetaja.Nimi = Console.ReadLine();
                        Console.WriteLine("Sisesta imetaja vanus:");
                        imetaja.Vanus = int.Parse(Console.ReadLine());
                        Console.WriteLine("Sisesta imetaja karvavärvus:");
                        imetaja.Karvavärvus = Console.ReadLine();
                        loomad.Add(imetaja);
                        break;
                    case "2":
                        Lind lind = new Lind();
                        Console.WriteLine("Sisesta linnu nimi:");
                        lind.Nimi = Console.ReadLine();
                        Console.WriteLine("Sisesta linnu vanus:");
                        lind.Vanus = int.Parse(Console.ReadLine());
                        Console.WriteLine("Sisesta linnu tiivaulatus:");
                        lind.Tiivaulatus = Console.ReadLine();
                        loomad.Add(lind);
                        break;
                    case "0":
                        jookseb = false;
                        break;
                    default:
                        Console.WriteLine("Vale valik, proovi uuesti.");
                        break;
                }
            }
            Console.WriteLine("Loomade söödamine:");
            foreach (Loom loom in loomad)
            {
                System.Console.WriteLine($"Näljatase: {loom.GetTervislikuSeisund()}");
                System.Console.WriteLine($"{loom.Nimi} ({loom.GetType().Name}) sööb:");
                loom.Söö();
            }
        }
    }
}