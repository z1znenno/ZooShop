namespace ZooShop
{
    public enum TervislikuSeisund
    {
        Kriitiline,  // näljatase < 30
        Rahuldav,    // näljatase < 60
        Hea          // näljatase >= 60
    }

    public enum Sugu
    {
        Isane,
        Emane,
        Teadmata
    }

    public enum LoomaStaatus
    {
        Kohal,     // elab meie loomaaias
        Transpordil,  // saadetud teise loomaaeda paljunemiseks
        Tagastatud    // naasis pärast paljunemist
    }
}
