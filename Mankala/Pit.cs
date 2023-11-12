namespace Mankala;

public class Pit : APit
{
    /// <summary>
    /// constructor for normal pit
    /// </summary>
    /// <param name="stones">stones in pit</param>
    /// <param name="index">index in original list</param>
    /// <param name="p">player who owns this pit</param>
    public Pit(int stones, int index,Player p)
        : base(stones, index, p)
    {
        Type = PitType.Pit;
    }
}