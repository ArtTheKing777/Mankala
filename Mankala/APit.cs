namespace Mankala;
public enum PitType 
{
    Pit,
    HomePit,
}
public abstract class APit
{
    public int Stones;
    public int Index;
    public Player Player;
    //maybes for if there are some weird game types
    public APit? Next;
    public APit? Previous;
    public APit? Opposite;
    public PitType Type;

    public APit(int stones, int index,Player p, APit? next = null, APit? previous = null,APit? opposite = null )
    {
        Stones = stones;
        Index = index;
        Next = next;
        Previous = previous;
        Opposite = opposite;
        Player = p;
    }
}