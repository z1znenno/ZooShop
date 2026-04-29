namespace ZooShop
{
    public class Lovi : Imetaja
    {
        public Lovi(string nimi, int vanus, int näljatase, string karvavärvus)
        {
            Nimi = nimi;
            Vanus = vanus;
            Näljatase = näljatase;
            Karvavärvus = karvavärvus;
        }
        public override void Söö()
        {
            System.Console.WriteLine("Lõvi sööb liha.");
            Näljatase += 20;
        }
        public void Raakida()
        {
            System.Console.WriteLine("Roar!");
        }
    }
}