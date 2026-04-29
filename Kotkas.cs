namespace ZooShop
{
    public class Kotkas : Lind
    {
        public Kotkas(string nimi, int vanus, int näljatase, string tiivaulatus)
        {
            Nimi = nimi;
            Vanus = vanus;
            Näljatase = näljatase;
            Tiivaulatus = tiivaulatus;
        }
        public override void Söö()
        {
            System.Console.WriteLine("Kotkas sööb kala.");
            Näljatase += 20;
        }
    }
}