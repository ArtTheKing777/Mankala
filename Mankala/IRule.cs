namespace Mankala;

/// <summary>
/// ruleset interface
/// </summary>
public interface IRule
{
    /// <summary>
    /// rules to determine who wins
    /// </summary>
    /// <param name="b">board to check</param>
    /// <returns>int who wins, 1 in player 1, 2 is player 2, 0 is a draw</returns>
    int DetermineWinner(IBoard b);

    /// <summary>
    /// check if the game has ended here
    /// </summary>
    /// <param name="b">board to check</param>
    /// <returns>bool whether the game has ended</returns>
    bool EndGame(IBoard b);
    
    /// <summary>
    /// get the moves that a player can perform
    /// </summary>
    /// <param name="b">board in question</param>
    /// <param name="p">player in question</param>
    /// <returns>a list of moves</returns>
    List<int> GetMoves(IBoard b,Player p);
}