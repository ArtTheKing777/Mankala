namespace Mankala;

public class WariRules : IRule
{
    /// <summary>
    /// rules to determine who wins
    /// </summary>
    /// <param name="b">board to check</param>
    /// <returns>int who wins, 1 in player 1, 2 is player 2, 0 is a draw</returns>
    public int DetermineWinner(IBoard b)
    {
        int p1Count = b.GetHomePits(Player.P1).Select(pit => pit.Stones).Sum(); // get haskelled
        int p2Count = b.GetHomePits(Player.P2).Select(pit => pit.Stones).Sum(); // get haskelled
        if (p1Count > p2Count) return 1; // player 1 wins
        if (p2Count > p1Count) return 2; // player 2 wins
        return 0; // draw
    }

    /// <summary>
    /// check if the game has ended here
    /// </summary>
    /// <param name="b">board to check</param>
    /// <returns>bool whether the game has ended</returns>
    public bool EndGame(IBoard b)
    {
        return GetMoves(b, Player.P1).Count + GetMoves(b, Player.P2).Count == 0;
    }

    /// <summary>
    /// get the moves that a player can perform
    /// </summary>
    /// <param name="b">board in question</param>
    /// <param name="p">player in question</param>
    /// <returns>a list of moves</returns>
    public List<int> GetMoves(IBoard b, Player p)
    {
        List<APit> pits = b.GetPlayerPits(p);
        List<int> moves = new List<int>();
        foreach (APit pit in pits)
        {
            if(pit.Stones > 0) moves.Add(pit.Index);
        }
        return moves;
    }
    
}