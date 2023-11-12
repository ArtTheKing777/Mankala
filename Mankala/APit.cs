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
    
    /// <summary>
    /// constructor for abstract pit, can be a normal pit or a home pit
    /// </summary>
    /// <param name="stones">stones in the pit</param>
    /// <param name="index">index in original list</param>
    /// <param name="p">player who's turn it is</param>
    public APit(int stones, int index,Player p)
    {
        Stones = stones;
        Index = index;
        Next = null;
        Previous = null;
        Opposite = null;
        Player = p;
    }
    
    /// <summary>
    /// connect across, usefull for stealing stones in mankala
    /// </summary>
    /// <param name="a">pit to connect to</param>
    public void ConnectCross(APit? a)
    {
        Opposite = a;
        if (a != null) { a.Opposite = this; }
    }
    
    /// <summary>
    /// regular connect, do to next pit in line
    /// </summary>
    /// <param name="a">pit to connect to</param>
    public void Connect(APit? a)
    {
        Previous = a;
        if (a != null) { a.Next = this; }
    }
}