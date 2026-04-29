namespace ZooShop
{
    public abstract class Loom : IToidetav
    {
        public string Nimi { get; set; }
        public int Vanus { get; set; }
        public int Näljatase
        {
            get;
            set
            {
                if (value < 0)
                {
                    Näljatase = 0;
                }
                else if (value > 100)
                {
                    Näljatase = 100;
                }
                else
                {
                    Näljatase = value;
                }
            }
        }
        public abstract void Söö();
        public string GetTervislikuSeisund()
        {
            if (Näljatase < 30)
            {
                return("Kriitiline — loom vajab kohe toitu!");
            }
            else if (Näljatase < 60)
            {
                return("Rahuldav");
            }
            else
            {
                return("Hea");
            }
        }
    }
}   