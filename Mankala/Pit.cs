namespace Mankala;

public class Pit : APit
{
    public Pit(int stones, int index,Player p, APit? next = null, APit? previous = null, APit? opposite = null)
        : base(stones, index, p, next, previous, opposite)
    {
        Type = PitType.Pit;
    }
}