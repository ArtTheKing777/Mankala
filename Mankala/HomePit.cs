namespace Mankala;

public class HomePit : APit
{
    public HomePit(int stones, int index,Player p)
        : base(stones, index, p)
    {
        Type = PitType.HomePit;
    }
}