namespace Mankala;

public class View
{
    /// <summary>
    /// Writes a line to the place where you will see it. in our case the console.
    /// </summary>
    /// <param name="s"></param>
    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }
    
    /// <summary>
    /// prints the board
    /// </summary>
    public void PrintBoard(Player player,IBoard board)
    {
        if (player == Player.P1)
        {
            Console.WriteLine(Reverser(PrintPlayerPits(Player.P2, board)));
            Console.WriteLine(PrintMiddleLine(board));
            Console.WriteLine(PrintPlayerPits(player, board));
        }
        else
        {
            Console.WriteLine(Reverser(PrintPlayerPits(Player.P1, board)));
            Console.WriteLine(PrintMiddleLine(board));
            Console.WriteLine(PrintPlayerPits(player, board));
        }
    }
    
    /// <summary>
    /// reverses a string
    /// </summary>
    /// <param name="s">string to reverse</param>
    /// <returns>reversed string</returns>
    private string Reverser(string s)
    {
        char[] c = s.ToCharArray();
        Array.Reverse(c);
        return new string(c);
    }
    
    /// <summary>
    /// helper function to printboard, handles the pits
    /// </summary>
    /// <param name="player">player who's side is printed</param>
    /// <param name="board">the current board</param>
    /// <returns>string in a format for display</returns>
    private string PrintPlayerPits(Player player, IBoard board)
    {
        List<APit> pits = board.GetPlayerPits(player);
        string _string = " ";
        int lenght = pits.Count - 1;
        for (int i = 0; i < lenght; i++)
        {
            _string += pits[i].Stones.ToString();
            _string += "|";
        }

        _string += pits[lenght].Stones;
        _string += " ";
        return _string;
    }

    /// <summary>
    /// helper function to printboard, handles deviding line and mankalas
    /// </summary>
    /// <returns>formatted string with line and mankalas</returns>
    private string PrintMiddleLine(IBoard board)
    {
        APit mankalaP2 = board.GetHomePits(Player.P2)[0];
        APit mankalaP1 = board.GetHomePits(Player.P1)[0];
        List<APit> pits = board.GetPlayerPits(Player.P1);
        string _string = "-";
        int lenght = pits.Count * 2 - 2;
        for (int i = 0; i < lenght; i++)
        {
            _string += "-";
        }
        return mankalaP2.Stones + _string + mankalaP1.Stones;
    }
}