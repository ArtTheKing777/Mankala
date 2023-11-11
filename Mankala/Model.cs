using System.Data;

namespace Mankala;

public class Model
{
    public IBoard board { get; private set; }
    private IRule rules;
    
    public void SetMode(string mode)
    {
        if (mode == "mankala")
        {
            board = new MankalaBoard();
            rules = new MankalaRules();
        }
        else if (mode == "wari")
        {
            board = new WariBoard();
            rules = new WariRules();

        }
        else if (mode == "newvariant")
        {
            board = new NewVariantBoard();
            rules = new NewVariantRules();
        }
    }

    public void MoveAmount(APit from, APit to, int amount) { board.MoveAmount(from,to,amount); }

    public List<APit> GetHomePits(Player p) { return board.GetHomePits(p); }
    public List<APit> GetPlayerPits(Player p) { return GetPlayerPits(p);}
    
    public List<APit> PitList { get { return board.PitList; } }

    public int DetermineWinner()
    {
        return rules.DetermineWinner(board);
    }

    public bool EndGame()
    {
        return rules.EndGame(board);
    }

    public List<int> GetMoves(Player p)
    {
        return rules.GetMoves(board, p);
    }

    public Player DoMove(Player p,int move)
    {
        return board.DoMove(p, move);
    }

}