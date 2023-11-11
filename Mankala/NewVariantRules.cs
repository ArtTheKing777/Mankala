namespace Mankala;

public class NewVariantRules : IRule
{
    public int DetermineWinner(IBoard b)
    {
        int p1Count = b.GetHomePits(Player.P1).Select(pit => pit.Stones).Sum();//get haskelled
        int p2Count = b.GetHomePits(Player.P2).Select(pit => pit.Stones).Sum();//get haskelled
        if (p1Count < p2Count) return 1;//player1Win
        if (p1Count > p2Count) return 2;//player2Win
        return 0;//draw
    }

    public bool EndGame(IBoard b)
    {
        return GetMoves(b,Player.P1).Count == 0 && GetMoves(b,Player.P2).Count == 0;
    }

    public List<int> GetMoves(IBoard b, Player p)
    {
        List<APit> Pits = b.GetPlayerPits(p);
        List<int> moves = new List<int>();
        foreach (APit pit in Pits)
        {
            if (pit.Stones > 0)
            {
                moves.Add(pit.Index);
                
                APit end = pit;
                for (int i = pit.Stones; i > 0; i--)
                {
                    end = end.Next;
                }
                if (end.Type == PitType.HomePit && end.Player == p)//forced to play if it occurs
                {
                    moves = new List<int>();
                    moves.Add(pit.Index);
                    return moves;
                }
                if (end.Type == PitType.Pit && end.Stones == 0 && end.Player == p)//forced to play if it occurs
                {
                    moves = new List<int>();
                    moves.Add(pit.Index);
                    return moves;
                }
            }
        }
        return moves;
    }
}