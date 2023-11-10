namespace Mankala;

public interface IRule
{
    int DetermineWinner(IBoard b);

    bool EndGame(IBoard b);

    List<int> GetMoves(IBoard b,Player p);
}