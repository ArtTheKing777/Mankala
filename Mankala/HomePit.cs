namespace Mankala;

public class HomePit : APit
{
    /// <summary>
    /// its a home pit, used for storing stones
    /// </summary>
    /// <param name="stones">stones in pit</param>
    /// <param name="index">index in original list</param>
    /// <param name="p">player who's pit it is</param>
    public HomePit(int stones, int index,Player p)
        : base(stones, index, p)
    {
        Type = PitType.HomePit;
    }
}