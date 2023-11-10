namespace Mankala;

public class HomePit : APit
{
    public HomePit(int stones, int index,Player p, APit? next = null, APit? previous = null, APit? opposite = null)
        : base(stones, index, p, next, previous, opposite)
    {
        Type = PitType.HomePit;
    }
}