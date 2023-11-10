namespace Mankala;

public class Pit : APit
{
    public Pit(int stones, int index,Player p)
        : base(stones, index, p)
    {
        Type = PitType.Pit;
    }
}