namespace ZooShop
{
    public class Imetaja : Loom
    {
        public string Karvavärvus { get; set; }
        public override void Söö()
        {
            System.Console.WriteLine("Imetaja sööb toitu.");
        }
        
    }
}