namespace Mankala;

public class WariRules : IRule
{
    public int DetermineWinner(IBoard b)
    {
        int p1Count = b.GetHomePits(Player.P1).Select(pit => pit.Stones).Sum(); // get haskelled
        int p2Count = b.GetHomePits(Player.P2).Select(pit => pit.Stones).Sum(); // get haskelled
        if (p1Count > p2Count) return 1; // player 1 wins
        if (p2Count > p1Count) return 2; // player 2 wins
        return 0; // draw
    }

    public bool EndGame(IBoard b)
    {
        return GetMoves(b, Player.P1).Count + GetMoves(b, Player.P2).Count == 0;
    }

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