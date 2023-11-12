using System.Data;

namespace Mankala;

public class Model
{
    public IBoard board { get; private set; }
    private IRule rules;
    
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mode">gamemode, can be mankala, wari, or newvariant</param>
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
    
    /// <summary>
    /// move an amount of pits from one pit to another
    /// </summary>
    /// <param name="from">pit to move from</param>
    /// <param name="to">pit to move to</param>
    /// <param name="amount">amount of stones to move</param>
    public void MoveAmount(APit from, APit to, int amount) { board.MoveAmount(from,to,amount); }

    /// <summary>
    /// gets home pits of a given player
    /// </summary>
    /// <param name="p">player to get home pits from</param>
    /// <returns>home pits of player p</returns>
    public List<APit> GetHomePits(Player p) { return board.GetHomePits(p); }
    
    /// <summary>
    /// get normal pits of a given player
    /// </summary>
    /// <param name="p">player to get pits from</param>
    /// <returns>pits of player p</returns>
    public List<APit> GetPlayerPits(Player p) { return GetPlayerPits(p);}
    
    /// <summary>
    /// pit list
    /// </summary>
    public List<APit> PitList { get { return board.PitList; } }

    /// <summary>
    /// rules to determine who wins
    /// </summary>
    /// <param name="b">board to check</param>
    /// <returns>int who wins, 1 in player 1, 2 is player 2, 0 is a draw</returns>
    public int DetermineWinner()
    {
        return rules.DetermineWinner(board);
    }

    /// <summary>
    /// check if the game has ended here
    /// </summary>
    /// <param name="b">board to check</param>
    /// <returns>bool whether the game has ended</returns>
    public bool EndGame()
    {
        return rules.EndGame(board);
    }

    /// <summary>
    /// get the moves that a player can perform
    /// </summary>
    /// <param name="b">board in question</param>
    /// <param name="p">player in question</param>
    /// <returns>a list of moves</returns>
    public List<int> GetMoves(Player p)
    {
        return rules.GetMoves(board, p);
    }

    /// <summary>
    /// asks for a move via DoMoveIO, and then does it on a board.
    /// </summary>
    /// <param name="player">player who will make the move</param>
    public Player DoMove(Player p,int move)
    {
        return board.DoMove(p, move);
    }

}