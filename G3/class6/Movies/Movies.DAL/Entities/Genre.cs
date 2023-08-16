namespace Movies.DAL.Entities
{
    [Flags]
    public enum Genre
    {
        None = 0,
        Comedy = 1,
        Action = 2,
        SciFi = 4,
        Horor = 8,
        //...
    }
}