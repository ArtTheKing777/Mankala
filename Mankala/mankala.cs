namespace Mankala;

public class mankala : Game
{
    
    /// <summary>
    /// standard mankala board, with 6 pits per person and 5 stones per pit.
    /// </summary>
    public mankala(GameManager g) : base(g)
    {
        _numberOfPits = 6;
        _startingStones = 4;
        board = new Board(_numberOfPits, _startingStones, new [] { _numberOfPits }, new [] { 2*_numberOfPits+1 });
    }
    
    /// <summary>
    /// custom size mankala board
    /// </summary>
    /// <param name="numberOfPits">The number of pits per person.</param>
    /// <param name="startingStones">The number of starting stones per pit.</param>
    public mankala(GameManager g, int numberOfPits, int startingStones) : base(g)
    {
        _numberOfPits = numberOfPits;
        _startingStones = startingStones;
        board = new Board(_numberOfPits, _startingStones, new [] { _numberOfPits }, new [] { 2*_numberOfPits });
    }
    /// <summary>
    /// override of the GetMoves function, for standard version of game
    /// </summary>
    /// <param name="p">player who's moves need to be gotten</param>
    /// <returns>int[] of indices that can be moved</returns>
    protected override int[] GetMoves(Player p)
    {
        int[] Pits = board.GetPitsOfPlayer(p);
        List<int> Moves = new List<int>();
        
        for (int i = 0; i < _numberOfPits; i++)
        {
            if (Pits[i] != 0) Moves.Add(i);
        }
        int[] moves = new int[Moves.Count];
        for (int i = 0; i < Moves.Count; i++)
        {
            moves[i] = Moves[i];
        }

        return moves;
    }

    /// <summary>
    /// does a move
    /// </summary>
    /// <param name="player">player who will do a move</param>
    protected override void DoMove(Player player)
    {
        Player notplayer; 
        if (player == Player.P1) notplayer = Player.P2; else notplayer = Player.P1;
        int[] moves = GetMoves(player);
        int move = moves[DoMoveIO(moves, player)];
        int amount = board.GetPitsOfPlayer(player)[move];
        if (player == Player.P2) move += _numberOfPits+1;
        int playermankala = board.GetPlayerMankalasIndencies(player)[0];
        int notplayermankala = board.GetPlayerMankalasIndencies(notplayer)[0];
        int i;
        for (i = 1; i <= amount; i++)//move+i=10 player=p2
        {
            if (move + i == notplayermankala) //drop stone in own mankala, but not others
            {
                amount++; 
                continue;
            }

            if (i == amount)//if final stone drops in empty pit on your side, you also get the stones on the other side of the board
            {
                int mankalap1 = board.GetPlayerMankalasIndencies(player)[0];
                int mankalap2 = board.GetPlayerMankalasIndencies(notplayer)[0];
                if ((i + move != mankalap1) && (i + move != mankalap2))
                {
                    if ((player == Player.P1 && move + i < playermankala) ||
                        (player == Player.P2 && move + i > notplayermankala))
                    {
                        if (board.GetPitsOfPlayer(player)[(move + i) % (_numberOfPits + 1)] == 0)
                        {
                            board.MoveAmount(move, playermankala, 1);
                            int opposite = (((_numberOfPits - (move + i)) * 2) + (move + i)) % (_numberOfPits*2+2);
                            board.MoveAmount(opposite, playermankala,
                                board.GetPitsOfPlayer(notplayer)[opposite - playermankala - 1]);
                            continue;
                        }
                    }
                }
            }
            board.MoveAmount(move, move + i, 1);
        }
        if ((move + i - 1) != playermankala) SwitchPlayerTurn();; //if final stone drops in mankala, get another turn
        
    }
    /// <summary>
    /// prints the board
    /// </summary>
    protected override void printBoard()
    {
        Console.WriteLine(reverser(PrintPlayerPits(Player.P2)));
        Console.WriteLine(PrintMiddleLine());
        Console.WriteLine(PrintPlayerPits(Player.P1));
    }
    /// <summary>
    /// reverses a string
    /// </summary>
    /// <param name="s">string to reverse</param>
    /// <returns>reversed string</returns>
    private string reverser(string s)
    {
        char[] c = s.ToCharArray();
        Array.Reverse(c);
        return new string(c);
    }
    /// <summary>
    /// helper function to printboard, handles the pits
    /// </summary>
    /// <param name="player">player who's side is printed</param>
    /// <returns>string in a format for display</returns>
    private string PrintPlayerPits(Player player)
    {
        int[] pits = board.GetPitsOfPlayer(player);
        string _string = " ";
        int lenght = pits.Length-1;
        for (int i = 0; i < lenght ; i++)
        {
            _string += pits[i];
            _string += "|";
        }
        _string += pits[lenght];
        _string += " ";
        return _string;
    }
    /// <summary>
    /// helper function to printboard, handles deviding line and mankalas
    /// </summary>
    /// <returns>formatted string with line and mankalas</returns>
    private string PrintMiddleLine()
    {
        int mankalaP2 = board.GetPlayerMankalas(Player.P2)[0];
        int mankalaP1 = board.GetPlayerMankalas(Player.P1)[0];
        string _string = "-";
        int lenght = _numberOfPits * 2 - 2;
        for (int i = 0; i < lenght; i++)
        {
            _string += "-";
        }
        return mankalaP2 + _string + mankalaP1;
    }

}